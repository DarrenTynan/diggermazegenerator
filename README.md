# Digger maze Generation
Using Unity3d engine and c sharp. A simple maze generation. Firstly filling screen with walls, then recursively digging out the walls to leave a maze path.

## What I learnt
The A* Search algorithm.
Improved my c sharp

## What could I improve?
Code recfactor the find neighbors method; instead of a buch of if's.
The most computational time is taken up by searching the openSet for the node with the lowest F cost. This could be optimized by using a heap data structure.

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
MIT
