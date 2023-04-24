## Third Person Shooter Controller

[PLAY ON ITCH](https://amitmelamed.itch.io/third-person-shooter)

We created third person shooter controller that can walk, crouch, sprint, aim down sight and fire bullets from his rifle using instantiate a bullet with real physics, including basic gun magazine bullet management system and reloading animations.
To move around and look use the WASD and mouse keys.
To ADS use the right mouse button.
To Crouch use the C key.
To Sprint hold the SHIFT key.
To fire use the left mouse button.
To Reload use the R key.
# Implementation of movement
We created the movementStateManager.cs script which will take care of gravity and WASD inputs, the script will always hold an instance of an abstract class called MovementBaseState which will be assign to one of the different Movement States our character have: Idle, Walking, Running and Crouching.

![image](https://user-images.githubusercontent.com/88790441/234091224-831edcb5-85b1-4ba1-8be9-59ebaa72e540.png)

 
Each of the different states is an implementation of the abstract class MovementBaseState:
 
 ![image](https://user-images.githubusercontent.com/88790441/234091241-4bfde042-5322-423b-a2cb-67439745433e.png)


When Entering and being in each state we will have different animaitons playing on our character using an animator which will control our lower body Rigged Character using Final State Machine in PlayerController.Controller (Base Layer).

![image](https://user-images.githubusercontent.com/88790441/234091258-1f759024-0105-4652-ab0f-b6c8068105c4.png)

 

The Aim state manager will be implementing the same concept and will control the upper part of the body including the arms.
We used different animation for the lower and upper body, which the upper body will hold a weapon and will be in Hip pos, ADS pos or Reloading pos.
We also used Final State Machine for the upper body animation control.

![image](https://user-images.githubusercontent.com/88790441/234091286-ad27025e-1cae-48f7-93cc-a96374db25f3.png)

 
We currently have the Hip Rifle, ADS and Reloading states.
We can control at which state we will be the same way we did in the Base Layer using the Aim State Manager (For Idle and ADS) and the Action State Manager which will control the weapon actions such as Reloading and Throwing grenades (in the future) .
# Head Movement
We Will the point which we are looking at at each frame in Update function at AimStateManager

![image](https://user-images.githubusercontent.com/88790441/234091316-8bed15fb-3c46-4b46-9435-cd822a914497.png)

 
And added:

![image](https://user-images.githubusercontent.com/88790441/234091332-fad04fda-66d7-4017-b97b-b73298e92005.png)

 
That will tell the Head bone to look always at the AimPosition calculated above.
# The Weapon
The Weapon is attached to the right hand of our character (AK47 Component).  
![image](https://user-images.githubusercontent.com/88790441/234091371-63dda052-0c34-4b3b-809f-2dfd2ef30cf1.png)

The Weapon In Inspector Variables:
![image](https://user-images.githubusercontent.com/88790441/234091383-919cea82-e1f5-4a42-8b35-d21bd0492e24.png)


 
The Weapon algorithm for shooting a bullet:
If:
1.We are at the fire rate time range
2.Have ammo in magazine
3.Pressed The Left Mouse button
Then we will call Fire which will Instantiate a bullet game object from the barrel position with and rotation that will face the direction of where we look at(Calculated at Head Movement section) , and add force using rigidbody component.
