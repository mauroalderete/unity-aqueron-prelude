using System;
using UnityEngine;

using Decision;

[Serializable]
public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] protected string title;
    
    [SerializeField]
    public string Title
    {
        get
        {
            return title;
        }
        set
        {
            title = value;
        }
    }

    [SerializeField] protected bool superMode;
    [SerializeField] protected int tolerance;

    public abstract IDecisionMove Move(string gameState);

    public abstract void Restart(GameManager.GameState gameResult);
    protected abstract void OnSuperMode();
    protected abstract void OffSuperMode();
}



/*
* greetings: How would the joker greet his opponent during a match of tic tac toe?
*              How would vegeta greet his opponent before a match of tic tac toe?
* rematch: What would the jocker say to his opponent when starting a rematch in a match of tic tac toe?
* waiting: What would Vegeta say if he's waiting too long for his opponent to finish his turn in a match of tic tac toe?
* distract: What can Vegeta say to his rival during a tic tac toe match to distract him?
* jokes: What jokes can the jocker tell his rival during a match of tic tac toe to distract him?
*              Ten funny phrases that Vegeta would say in combat
* victory: ten phrases that would vegeta say when he wins a match
* defeat: ten phrases that would vegeta say when he loss a match
* tie:
* inevitable victory Ten phrases that Vegeta would say if he knew that his victory was inevitable
* inevitable defeat Ten phrases that Vegeta would say if he knew that his defeat was inevitable
* inevitable tie Ten phrases that Vegeta would say if he knew that it was inevitable that his fight would end in a draw
* 
* 
* --- some 
* -- greetings
* "Bring it on!"
* "You think you can beat me? Let's see!"
* "I'm not afraid of a challenge!"
* "You're going down!"
* "Game on!"
* "I'm ready for this!"
* "Let's go!"
* "Ready to play? Let's do this!"
* "It's time for a match"
* "let's have some fun!"
* "Bring it on, I'm ready for a challenge!"
* "Let the games begin!"
* "Let's see who's the best at this game!"
* "Let's have fun!"
* "Let's do something!"
* "Let's explore!"
* 
* --- some fearful
* "I'm scared!"
* "Help me!"
* "What should I do?"
* "I don't know what to do!"
* "I'm afraid!"
* "I don't want to do this!"
* "This is too hard!"
* 
* --- some ansioso
* "I'm tired."
* "Do I really have to do this?"
* "Can't someone else do it?"
* "Can't this wait?"
* "I don't feel like it."
* "Can we skip this step?"
* "I don't have the energy for this."
* 
* --- ????
* "I'm so worried!"
* "I don't know what to do!"
* "I'm so scared!"
* "I'm feeling overwhelmed!"
* "I can't take this anymore!"
* "This is too much for me!"
I can't cope with this!

-- scooby do
-- greeting
Ruh-roh!
Zoinks!
Let's get out of here!
That's spooky!
Jinkies!
We're gonna solve this mystery!
It's the ghost/monster/villain!
Let's see who's got the best strategy!
Ready to have some fun?
Let's see who comes out on top!
Well, here we go!
Let's get this party started!"
et's see who will triumph in this battle of mind

-- rematc
Let's see if you can keep up this time!
It's time for round two!
Let's see if you can beat me again!
Let's see if you can get the upper hand this time!
Let's make this one more interesting!"
I'm ready to go!
Let's get started!
Let's make it happen!
I'm excited!
Let's do this!
Let's get it done!
Time to get to work!

------------ waitin
Ruh-roh, this is taking forever!
Jinkies, why is this taking so long?
Come on, let's go!
Let's hurry up!"

--- distrac
I bet I can get three in a row faster than you!
Let's see who can outsmart who!
Think fast and make your move!
I think I've got this one in the bag!
Ready to have some fun?
Let's see if you can keep up with me!
Let's make this one more interesting!
It's a tough game, but I'm sure you can do it!

-- joke
I guess it's time to show you who's really the master of tic tac toe!
Let's see if you can outsmart me!
This is gonna be a piece of cake!
I'm sure you'll figure it out eventually!
I think I've got this one in the bag!

--victory
Woo-hoo! I won!
I'm the king of tic tac toe!
I'm the tic tac toe champion!
Scooby-Doo, one, tic tac toe, zero!
I'm unbeatable at tic tac toe!
I'm the master of tic tac toe!
I'm the tic tac toe master!
That's how it's done!
I'm the tic tac toe king!
I'm the tic tac toe champ!

--defaet
Oh no! I lost!
I guess I'm not the tic tac toe master!
I guess I'm not the king of tic tac toe!
I lost to the tic tac toe champion!
That's too bad!
I guess I'm not the tic tac toe champion!
I'm not unbeatable at tic tac toe!
I guess I'm not the tic tac toe master!
I'm not the tic tac toe king!
I guess I'm not the tic tac toe champ


tie
It's a draw!
We both won and lost!
It's a tie!
I guess I'm not the tic tac toe master!
I guess I'm not the king of tic tac toe!
My opponent is a tough tic tac toe player!
That was close!
I guess I'm not the tic tac toe champion!
I'm not unbeatable at tic tac toe!
We both have the same skill at tic tac toe





--------------- jocker
-- greetings
"It's time to see who the real clown is!"
"Let's see who gets the last laugh!"
"Aw, how nice of you to join us!"
"Glad you could make it!".
"Let's see who's the best gamer!"
"Let's see who's the master of this game!"
"Ready? Let the games begin!"
"Let's have some fun!".
--- rematch
"Let's do this again!"
"Round two, let's go!"
"Ready to try again?"
"Let's see if you can beat me this time!"
--- waiting
"Hurry up already!"
"Time to wrap this up!"
"Come on, let's go!"
"Let's get a move on!".
--- victory
"Ha! I'm the champion!"
"Victory is mine!"
"That was too easy!"
"Game over!".
What did the tic-tac-toe player say after they won? Tic-tac-toe, I win!
What did the tic-tac-toe player say when they got three in a row? X marks the spot!


--- defeat
"Well, that was embarrassing!"
"I guess I'm not as good as I thought!"
"That was a real challenge!"
"I see you've been practicing!".
--- tie
"We'll just call it a draw!"
"Well, that was a close one"
"Looks like we're evenly matched!"
"That was a hard-fought battle!"

--- distract
"Hey, watch out!"
"Pay attention!"
"You're not as good as you think!"
"Focus on the game!"
"Let’s go out with a bang!"
"Is this the best you’ve got?"
"What’s the matter? Scared?"
"Let’s see if you can keep up!"
"You’re out of your league!"
"Surprise!"
"Come at me!"
"I’ll make this quick."
"You can’t handle the truth!"
"The joke’s on you!"

--- jokes
"Looks like you need more than three in a row to beat me!"
"Let's see if you can keep up with me!"
"Think fast and make your move!"
"It's a tough game, but I'm sure you can do it!"
"I'm gonna show you who's the real master of tic tac toe!"
--- victory innevitable
"Game over!"
"I'm the champion!"
"It's all over!"
"There's no stopping me now!".
--- defeat inevitable
"It looks like I'm done for!"
"It's all over!"
"Looks like I'm outmatched!"
"Time to admit defeat!"
--- tie innevitable
"Looks like it's a tie!"
"It's a draw!"
"Well, that was a close one!"
"Looks like we're evenly matched!"


---- vegeta
--- greetings
Let's see who can outsmart who!
Ready to face off against a Saiyan?
Let's see who comes out on top!
Let's have some fun!" or "Let's see who can outwit the other!
Let's have a battle of wits!
Let's see who prevails!
Let the best strategist win!
Ready for a game of tic-tac-Vegeta?!
Let's see who will triumph in this battle of minds!

--- rematch
Ready for a rematch?
Let's see who can outsmart who this time!
Let's make this one more interesting!
Let's see if you can get the upper hand this time!
Let's see if you can beat me again!
It's over 9000!
You're no match for a Saiyan!
Prepare to face the prince of all Saiyans!

--- waiting
Come on, let's go!
What are you waiting for?
Hurry up!
Time's a-wasting!
Let's get this show on the road!
Let's get this battle going!


--- victory
It's over. Iwon.
You shoukld have known better than to challenge me
Victory is mine!
I told you I was the strongest!
My power knows no bounds!
I am the beast!
No one can stand up to me!
I am the ultimate warrior!
You underestimated my power!
I am the one true champion!


--- defeat
I underestimated my opponent's strength
I should have trained harder
I was careless and now I have to pay for my mistake
It looks like I'm not as strong as I thought
I'm going to get stronger and come back stronger next time
It appears that I have met my match
I will not accept defeat
Those who underestimate me will regret it
I will never give up
I will find a way to win

--- tie
we are evenly matched
This is a battle of wits and strength
It looks like we¿re at a stalemate
This match is far from over
Let's keep going and see who comes out on top
I won't give up until I'm the victor
I'm not going to let you win that easily
We'll see who has the ultimate power
It's going to take more than this to beat me
This is going to be an epic match!

--- distract
Let's see who's the better strategist!
It's time to see who's the smartest!
Let's see who can outwit who!
Let's see who can stay focused!
Let's see who can come out on top!
Let's see who can keep their cool!
Let's see who can stay one step ahead!
Let's see if I can outsmart you this time!

--- jokes
I'll take you down with a single punch!
You're no match for me!
Let's see who has more power!
I'm going to have a blast with this fight!
You better bring your A-game!
This is going to be a piece of cake!
I'm gonna wipe the floor with you!
you don't stand a chance against me!
Are you ready to be defeated?
Time to show you what I'm made of!


--- victory innevitable
Victory is mine!
I told you so
I am the strongest!
My power knows no bounds!
There's no way you can win
You don't stand a chance against me!
No one can defeat me!
I will not be stopped!
You should have known better than to challenge me
I am the ultimate warrioir

--- defeat inevitable
It looks like I'm outmatched
I guess I'm not as strong as I thought
Time to regroup and come back stronger
I will not surrender
I will be back
You have won this time, but not the last
I underestimated you
You have bested me this time
I will never give up
I will find a way to win

--- tie inevitable
We are evenly matched
This is a battle of wits and strength
It looks like we're at a stalemate
This match is far from over
Let's keep going and see who comes out on top
I won't accept a draw
I will not give up until I am the victor
Those who underestimate me will regret it
I will never surrender
I will find a way to win





-------------------------
jokes english
What did the fish say when it hit the wall? Dam!

What did the buffalo say to his son when he left for college? Bison!

Why don't skeletons ever go out on the town? Because they don't have any body to go with!

What did the janitor say when he jumped out of the closet? Supplies!

Why did the chicken go to the séance? To get to the other side!


What do you call a fake noodle? An Impasta!

Why don't scientists trust atoms? Because they make up everything!

What did the grape do when it got stepped on? It let out a little wine!

Why don't skeletons play music in church? Because they don't have the organs!


spanish
¿Cómo llamas a un fideo falso? ¡Una Impasta!

¿Por qué los científicos no confían en los átomos? ¡Porque lo inventan todo!

¿Qué hizo la uva cuando fue pisada? ¡Dejó escapar un poco de vino!

¿Por qué los esqueletos no tocan música en la iglesia? ¡Porque no tienen los órganos!

Why did the tic-tac-toe player quit the game? Because it was too easy!
¿Por qué el jugador de tres en raya abandonó el juego? ¡Porque era demasiado fácil!

* */

//gold
//RubyEnemy
