# RPG-mechanics

This is an in-progress project with some RPG mechanics, also connected with Playfab.

How to use it.

You need Unity 2020.3.30f1

After hitting play, you are going to see a login screen, also you have the option to create an account.
Then you can play the game or check the leaderboard (there are no entries yet but it is fetched from Playfab).

You can skip the Login and the Menu by going to EDITORHACKS object and enabling Skip Game Menu.

In the game screen, you have the possibility to move, using WASD, range shooting with mouse left click and melee attack with the space bar. 
You have an inventory panel as well, to use it, you can go to Assets/Scripts/GameData/Inventory_Player and it has 2 methods to add health and mana potion to the inventory, use the quantity field to change the amount you want to give to the player.

You also can find and change those objects by going to Assets/Resources/Items/Consumables, you can change the Sell Prize (no use yet), the Max Stack, Icon and Name, and Rarity, also you can create new items by going to the Create menu "Items/Consumable Item" and use the same methods in the inventory, the system also supports rarity Assets/Resources/Items/Rarity you can set this rarity on each item.

The inventory supports drag and drop, swap items, and you drop the item outside the panel you can destroy, it uses the Max Stack of each item to split on stacks, when you hover an item on the inventory it shows a small panel with the item info.

I hope you like it. I'm open to hearing recommendations.

I will continue working on this...
