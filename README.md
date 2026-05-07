COntrol:
 Drag and release shooting mechanics
 Real-time trajectory prediction
 Precision Mode with angle and speed validation
 Ball shatter effect on failed impacts
 Score and lives system
 Mobile touch controls

Simple Mode
 Drag to aim and release to shoot.
 Follow surfaces and reach the target box.
 Incorrect angle or excessive speed will shatter the ball.

Precision Mode
 The first impact must land on a Safe Pad (Green Color).
 Reach the target box to score points.

Implementation:
 The project was implemented using Unity 2D physics and C# scripting.
 Drag-and-release shooting mechanic
 Real-time trajectory prediction using LineRenderer
 Surface-follow interaction logic
 Ball shatter effect using fractured prefabs
 Score and lives system
 Scene-based game mode handling
 Android touch input support

Surface Interaction Logic
 Ball velocity
 Collision angle
 
 When the ball collides with a surface:
  The collision angle is calculated using the surface normal and ball velocity.
  The impact speed is checked against allowed thresholds.
  If both conditions are valid, the ball follows the surface direction.
  Otherwise, the ball shatters and the player loses a life.
  Precision Mode add an additional condition where the first collision must occur on a Safe Pad.


Challenges:
  Handling physics-based movement and maintain stable gameplay behavior
  Implementing smooth collision response on curved
  Preventing unwanted multiple collision triggers

Improvement if more time is given
 Better visual effects and particles
 Sound effects and background music
 Improved UI 
 Replace the hard rigid ball with a soft-body


Technologies Used
 Unity 2D
 C#