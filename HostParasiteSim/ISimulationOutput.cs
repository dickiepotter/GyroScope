using System;

public interface ISimulationOutput
{
	void Add
	(
		int simulation, 
		int time, 
		int parasite, 
		float xPosition, 
		float yPosition,
		float resources,
		float constitution
	);

	
}
