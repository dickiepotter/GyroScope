'use strict';

const form = document.getElementById('params');
const canvas = document.getElementById('arena');
const ctx = canvas.getContext('2d');

const playBtn = document.getElementById('playBtn');
const restartBtn = document.getElementById('restartBtn');
const scrub = document.getElementById('scrub');
const speed = document.getElementById('speed');
const speedVal = document.getElementById('speedVal');
const autoRun = document.getElementById('autoRun');

const elError = document.getElementById('error');
const elTruncated = document.getElementById('truncated');
const statTime = document.getElementById('statTime');
const statPop = document.getElementById('statPop');
const statCon = document.getElementById('statCon');
const statRes = document.getElementById('statRes');
const statSeed = document.getElementById('statSeed');

// Current run state.
let result = null;       // server response
let frameIndex = 0;
let playing = false;
let lastTick = 0;
let maxResources = 1;    // for scaling dot size, derived per run

function readParams() {
    const data = new FormData(form);
    const p = {};
    for (const [k, v] of data.entries()) {
        p[k] = Number(v);
    }
    return p;
}

async function run() {
    elError.hidden = true;
    const params = readParams();
    let res;
    try {
        res = await fetch('/api/run', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(params)
        });
    } catch (e) {
        return showError('Could not reach the server.');
    }

    if (!res.ok) {
        let msg = 'Simulation failed.';
        try { msg = (await res.json()).error || msg; } catch { /* ignore */ }
        return showError(msg);
    }

    result = await res.json();
    elTruncated.hidden = !result.truncated;
    statSeed.textContent = result.seed < 0 ? 'random' : result.seed;

    // Derive a resources scale for dot sizing from the whole run.
    maxResources = 1;
    for (const f of result.frames) {
        for (const par of f.p) {
            if (par.r > maxResources) maxResources = par.r;
        }
    }

    scrub.max = Math.max(0, result.frames.length - 1);
    frameIndex = 0;
    scrub.value = 0;
    setPlaying(result.frames.length > 1);
    render();
}

function showError(message) {
    elError.textContent = message;
    elError.hidden = false;
    result = null;
    setPlaying(false);
}

function setPlaying(on) {
    playing = on;
    playBtn.textContent = on ? '⏸' : '▶'; // pause / play
    if (on) {
        lastTick = performance.now();
        requestAnimationFrame(loop);
    }
}

function loop(now) {
    if (!playing || !result) return;
    const fps = Number(speed.value);
    if (now - lastTick >= 1000 / fps) {
        lastTick = now;
        frameIndex++;
        if (frameIndex >= result.frames.length) {
            frameIndex = result.frames.length - 1;
            scrub.value = frameIndex;
            render();
            setPlaying(false);
            return;
        }
        scrub.value = frameIndex;
        render();
    }
    requestAnimationFrame(loop);
}

function colourFor(constitution) {
    // Health: full constitution -> green, zero -> red.
    const t = Math.max(0, Math.min(1, constitution / (result.maxConstitution || 1)));
    const hue = t * 125; // 0 (red) .. 125 (green)
    return `hsl(${hue} 75% 55%)`;
}

function render() {
    if (!result) return;
    const W = canvas.width;
    const H = canvas.height;
    const pad = 28;
    const arenaW = W - pad * 2;
    const arenaH = H - pad * 2;

    ctx.clearRect(0, 0, W, H);

    // Immuno-grid cells.
    ctx.strokeStyle = 'rgba(255,255,255,0.06)';
    ctx.lineWidth = 1;
    ctx.beginPath();
    for (let c = 0; c <= result.cols; c++) {
        const x = pad + (arenaW * c) / result.cols;
        ctx.moveTo(x, pad);
        ctx.lineTo(x, pad + arenaH);
    }
    for (let r = 0; r <= result.rows; r++) {
        const y = pad + (arenaH * r) / result.rows;
        ctx.moveTo(pad, y);
        ctx.lineTo(pad + arenaW, y);
    }
    ctx.stroke();

    // Border of the host.
    ctx.strokeStyle = 'rgba(255,255,255,0.18)';
    ctx.strokeRect(pad, pad, arenaW, arenaH);

    const frame = result.frames[frameIndex];
    if (!frame) return;

    // A lineage shares one Position object in the engine, so many parasites sit
    // on the exact same coordinate. Group co-located parasites, fan them out into
    // a small cluster so individuals are visible, and badge the count — this is a
    // display aid only; it does not alter the simulation data.
    const groups = new Map();
    let sumC = 0;
    let sumR = 0;
    for (const par of frame.p) {
        const key = par.x + '|' + par.y;
        let g = groups.get(key);
        if (!g) { g = { x: par.x, y: par.y, members: [] }; groups.set(key, g); }
        g.members.push(par);
        sumC += par.c;
        sumR += par.r;
    }

    for (const g of groups.values()) {
        const cx = pad + (g.x / result.width) * arenaW;
        const cy = pad + arenaH - (g.y / result.length) * arenaH;
        const count = g.members.length;

        if (count === 1) {
            const m = g.members[0];
            drawParasite(cx, cy, m, 3 + 5 * Math.sqrt(Math.max(0, m.r) / maxResources));
            continue;
        }

        // Fan the members out on a phyllotaxis spiral around the shared point.
        // Cap how many dots we actually draw so a huge cluster stays fast and on
        // screen; the badge always reports the true count.
        const maxDots = 60;
        const drawn = Math.min(count, maxDots);
        const spacing = 4.5;
        let spread = 0;
        for (let i = 0; i < drawn; i++) {
            const angle = i * 2.3999632; // golden angle (radians)
            const r = spacing * Math.sqrt(i);
            if (r > spread) spread = r;
            drawParasite(cx + r * Math.cos(angle), cy + r * Math.sin(angle), g.members[i], 3);
        }

        drawCountBadge(cx, Math.max(pad + 10, cy - spread - 12), count);
    }

    const n = frame.p.length;
    statTime.textContent = frame.t;
    statPop.textContent = n;
    statCon.textContent = n ? (sumC / n).toFixed(2) : '–';
    statRes.textContent = n ? (sumR / n).toFixed(2) : '–';
}

function drawParasite(px, py, par, radius) {
    ctx.beginPath();
    ctx.fillStyle = colourFor(par.c);
    ctx.globalAlpha = 0.9;
    ctx.arc(px, py, radius, 0, Math.PI * 2);
    ctx.fill();
    ctx.globalAlpha = 1;
}

function drawCountBadge(cx, cy, count) {
    const label = '×' + count;
    ctx.font = '600 12px -apple-system, "Segoe UI", Roboto, sans-serif';
    ctx.textAlign = 'center';
    ctx.textBaseline = 'middle';
    const w = ctx.measureText(label).width + 12;
    const h = 17;
    pill(cx - w / 2, cy - h / 2, w, h, h / 2);
    ctx.fillStyle = 'rgba(13,17,23,0.88)';
    ctx.fill();
    ctx.strokeStyle = 'rgba(255,255,255,0.28)';
    ctx.lineWidth = 1;
    ctx.stroke();
    ctx.fillStyle = '#e6edf3';
    ctx.fillText(label, cx, cy + 0.5);
}

function pill(x, y, w, h, r) {
    ctx.beginPath();
    ctx.moveTo(x + r, y);
    ctx.arcTo(x + w, y, x + w, y + h, r);
    ctx.arcTo(x + w, y + h, x, y + h, r);
    ctx.arcTo(x, y + h, x, y, r);
    ctx.arcTo(x, y, x + w, y, r);
    ctx.closePath();
}

// --- Controls ---

playBtn.addEventListener('click', () => {
    if (!result) return;
    if (!playing && frameIndex >= result.frames.length - 1) frameIndex = 0;
    setPlaying(!playing);
});

restartBtn.addEventListener('click', () => {
    if (!result) return;
    frameIndex = 0;
    scrub.value = 0;
    render();
});

scrub.addEventListener('input', () => {
    if (!result) return;
    setPlaying(false);
    frameIndex = Number(scrub.value);
    render();
});

speed.addEventListener('input', () => {
    speedVal.textContent = `${speed.value} fps`;
});

form.addEventListener('submit', (e) => {
    e.preventDefault();
    run();
});

// Debounced auto-run on edit (the "real-time editing" feel).
let debounce = null;
form.addEventListener('input', (e) => {
    if (e.target === speed || e.target === scrub || e.target === autoRun) return;
    if (!autoRun.checked) return;
    clearTimeout(debounce);
    debounce = setTimeout(run, 400);
});

// Initial run on load.
run();
