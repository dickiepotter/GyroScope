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

    let sumC = 0;
    let sumR = 0;
    for (const par of frame.p) {
        const px = pad + (par.x / result.width) * arenaW;
        // Invert Y so the origin sits at the bottom-left.
        const py = pad + arenaH - (par.y / result.length) * arenaH;
        const radius = 3 + 5 * Math.sqrt(Math.max(0, par.r) / maxResources);

        ctx.beginPath();
        ctx.fillStyle = colourFor(par.c);
        ctx.globalAlpha = 0.9;
        ctx.arc(px, py, radius, 0, Math.PI * 2);
        ctx.fill();
        ctx.globalAlpha = 1;

        sumC += par.c;
        sumR += par.r;
    }

    const n = frame.p.length;
    statTime.textContent = frame.t;
    statPop.textContent = n;
    statCon.textContent = n ? (sumC / n).toFixed(2) : '–';
    statRes.textContent = n ? (sumR / n).toFixed(2) : '–';
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
