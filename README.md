## Third Person Shooter Controller

[PLAY ON ITCH](https://amitmelamed.itch.io/third-person-shooter)

We created third person shooter controller that can walk, crouch, sprint, aim down sight and fire bullets from his rifle using instantiate a bullet with real physics, including basic gun magazine bullet management system and reloading animations.
To move around and look use the WASD and mouse keys.
To ADS use the right mouse button.
To Crouch use the C key.
To Sprint hold the SHIFT key.
To fire use the left mouse button.
To Reload use the R key.
#Implementation of movement
We created the movementStateManager.cs script which will take care of gravity and WASD inputs, the script will always hold an instance of an abstract class called MovementBaseState which will be assign to one of the different Movement States our character have: Idle, Walking, Running and Crouching.
 
Each of the different states is an implementation of the abstract class MovementBaseState:
 

When Entering and being in each state we will have different animaitons playing on our character using an animator which will control our lower body Rigged Character using Final State Machine in PlayerController.Controller (Base Layer).
 

The Aim state manager will be implementing the same concept and will control the upper part of the body including the arms.
We used different animation for the lower and upper body, which the upper body will hold a weapon and will be in Hip pos, ADS pos or Reloading pos.
We also used Final State Machine for the upper body animation control.
 
We currently have the Hip Rifle, ADS and Reloading states.
We can control at which state we will be the same way we did in the Base Layer using the Aim State Manager (For Idle and ADS) and the Action State Manager which will control the weapon actions such as Reloading and Throwing grenades (in the future) .
#Head Movement
We Will the point which we are looking at at each frame in Update function at AimStateManager
 
And added:
 
That will tell the Head bone to look always at the AimPosition calculated above.
#The Weapon
The Weapon is attached to the right hand of our character (AK47 Component).   
The Weapon In Inspector Variables:
 
The Weapon algorithm for shooting a bullet:
If:
1.We are at the fire rate time range
2.Have ammo in magazine
3.Pressed The Left Mouse button
Then we will call Fire which will Instantiate a bullet game object from the barrel position with and rotation that will face the direction of where we look at(Calculated at Head Movement section) , and add force using rigidbody component.
