# Room interior designer

This app is focused on creating your own interior design. Object is created after pressing on a selected item at the bottom of screen.

Currently available furniture/objects:
 - **Chair** - can be placed on the Floor
 - **Table** - can be placed on the Floor
 - **Plate with fruits** - can be placed on a Chair, Table or Floor
 - **2 Pictures** - can be placed on the Walls
 
 After snapping object in a desired place, you can open furniture editing features by clicking on the object you want to change. Features provided:
  - Move
  - Rotate (you can rotate object while holding it as well by using Mouse scrollwheel)
  - Change color
  - Delete
  
New/Save/Load features:
 - New/Save/Load buttons are located in the top right corner.
 - **New** button loads new empty room, everything from the current design is deleted.
 - **Save** button lets to enter room design name and saves current properties as .json file inside *~AppData/LocalLow/DefaultCompany/YKAE/RoomFiles* folder.
 - **Load** button lists all saved room designs inside *RoomFiles* folder. Selected room is loaded, current changes are not saved.
 
 
*Build* & *Example Room Json* (needs to be moved to *~AppData/LocalLow/DefaultCompany/YKAE/RoomFiles* folder) are Located inside **Build** folder.
