echo off
echo "sample grid 3.1, args : 2 2 1,1 0,0 1,0" && RoverAssignment 2 2 1,1 0,0 1,0 && echo:
echo:
echo "sample grid 3.3.1, args : 3 2 1,1 0,1 0,2" && RoverAssignment 3 2 1,1 0,1 0,2 && echo:
echo:
echo "sample grid 3.3.2, args : 4 3 1,0 2,2 0,3 1,1" && RoverAssignment 4 3 1,0 2,2 0,3 1,1 && echo:
echo:
echo "sample grid 3.3.3, args : 8 5 5,4 6,6 1,0 0,5 5,1 4,6" && RoverAssignment 8 5 5,4 6,6 1,0 0,5 5,1 4,6 && echo:
echo:
echo "random backtrace test, args : 6 3 3,3 0,0 5,5 5,0" && RoverAssignment 6 3 3,3 0,0 5,5 5,0 && echo:
echo:
echo "Worst case scenario 1 component, args : 5 1 4,4 4,4" && RoverAssignment 5 1 4,4 4,4 && echo:
echo:
echo "Worst case scenario 2 components, args : 5 2 4,4 0,0 4,4" && RoverAssignment 5 2 4,4 0,0 4,4 && echo:
echo:
echo "Worst case scenario 3 components, args : 5 3 4,4 0,0 4,3 4,4" && RoverAssignment 5 3 4,4 0,0 4,3 4,4 && echo:
PAUSE
