# 3d-team-game-ngage
3d-team-game-ngage created by GitHub Classroom

  The game starts off with the player on the middle island. The player will have to jump from island to island. Certain islands have a generator on it that feed power to the force field. To destroy the force field, the player needs to kill all the enemies that are on that specific island. When all the generators are destroyed the force field will disappear, and the dragon will be released. 
  The dragon will periodically gather energy to unleash his plasma fireball at the player. The fireball itself does significant damage to the player, and the explosion of the fireball hurts the player but to a lesser degree. The player must either lose the game by dying or win the game by killing the dragon.  We have a helpful tutorial to teach the games controls to the player.

Issues and Solutions

Our Issues and Solutions can be broken down into these 4 categories; Island Generation, Player, Island Enemy, and Dragon Boss.

-Island Generation

Our initial plan for our game was to have the player jump from island to island and destroy the generators located on them to release the boss. The islands needed to be floating in outer space and they needed to be located around the island that the boss was located on. We knew that creating each island individually would take too much time so we opted for using a single island prefab and spawning in multiple copies of it through code. This proved to be easier to implement than first thought, so we decided to spice up the islands with floating movement. The islands now gently floated in both directions vertically as they also floated horizontally around the center point being the Boss. Adding new features like the island enemy spawner and the generator proved to be easy due to the fact that all the islands were instantiated from the same prefab. Eventually we ran into a problem while testing the game, the players screen would shake while standing on an island. This was caused from the islands movement, and the players position not being updated fast enough. As a solution we decided to make the player detect if they were standing on an island, and if they were then they would make themselves a child object of that island. This caused another problem however, being that whenever the player was made the child of an island they would end up taking on that island’s rotation. This would cause the player to turn in random direction upon entering an islands collider, making it impossible to land on those islands. After trying a bunch of possible solutions we finally reached an answer. We would spawn an object in front of the player right before they were made into a child object, and afterwards make the player look at it. We also removed the OnTriggerExit() from player to tone down the rotation issue. This means that even if the player jumps off of an island they are still child objects of that island until they land onto another island. Which means that whatever happens to that parent island will also happen to the player, this includes rotation and transformations.  

-Player 
-Island Enemy 
-Dragon Boss


