# RocketCoordinateCheckerAPI
An API designed for a hypothetical Rocket launching system.
- Project is designed in VS2019.
- Swagger is implemented for both documentation and TEST purposes.
- Simply hit "F5"  on VS2019 and you will be able to execute all functions.


Assuming that landing area has size of square 100x100 and landing platform has a size of a square 10x10 
and it's top left corner starts at a position 5,5 (please assume that position 0,0 is located at 
the top left corner of landing area and all positions are relative to it), library should work as 
follows: 
• if rocket asks for position 5,5 it replies `ok for landing`
• if rocket asks for position 16,15, it replies `out of platform`
• if the rocket asks for a position that has previously been checked by another rocket
(only last check counts), it replies with `clash`
• if the rocket asks for a position that is located next to a position that has previously
been checked by another rocket (say, previous rocket asked for position 7,7 and the
rocket asks for 7,8 or 6,7 or 6,6), it replies with `clash`.Given the above.

API supports following features:
• rocket can query it to see if it's on a good trajectory to land at any moment
• library can return one of the following values: 'out of platform', 'clash', 'ok for landing'
• more than one rocket can land on the same platform at the same time and rockets need to have at least one unit separation between their landing positions 
• platform size can vary and should be configurable



TEST Screen :
Whenever you execute the project in

![Screenshot_1](https://user-images.githubusercontent.com/49819371/137634204-51441bdd-1cd6-4636-9620-c8a9d82fb1c0.jpg)

