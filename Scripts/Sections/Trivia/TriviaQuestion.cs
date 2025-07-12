using System.Collections.Generic;

namespace SpireQuiz.Scripts.Sections.Trivia;

public class TriviaQuestion
{
	public string Text;
	public bool Answered;
	public int TimesAppeared;
	
	public TriviaQuestion(string text)
	{
		Text = text;
	}

	public static List<TriviaQuestion> AllQuestions => new()
	{
		"1.    What is the name of the Relic which appears in place of new Relics when the Relic pool is exhausted?",
		"2.    What is the name of the Curse which can be gained only as part of a Blight in Endless mode?",
		"3.    How many Stances does Watcher have?",
		"4.    What is the first name of either of the developers of Slay the Spire?",
		"5.    How much maximum health does the player lose when removing Parasite?",
		"6.    What is the name of the Status applied by The Spire Shield and Spear?",
		"7.    What character currently has the fastest world record any% unseeded speedrun?",
		"8.    When Maxx first held the world record for the longest Watcher winstreak, how long was the streak?",
		"9.    How many unique gremlin enemies are there?",
		"10.    Without any modifiers, what is the maximum damage unupgraded Flechettes can deal?",
		"11.    What is the name of the card with the highest mana cost?", "12.    What is the flavor text of Akabeko?",
		"13.    What is the name of the Score reward granted for having 4 copies of a card?",
		"14.    What is the only enemy that can appear in all 3 acts?",
		"15.    Finishing which enemy with Feed grants an achievement?",
		"16.    What Spire character was the first one to become a plushe?",
		"17.    The Watcher appears as a playable character in which Fighting Game?",
		"18.    The card title “Reach Heaven/Through Violence” is a reference to what webcomic?",
		"19.    What is Silent’s homeland called?", "20.    Which relic replaces Cracked Core?",
		"21.    When starting a fresh save file, what is the first boss the player will encounter?",
		"22.    Name a Defect card that CAN’T be gained from random card generation effects?",
		"23.    Which card has art featuring a Rubix cube?",
		"24.    What year did this discord server first hold a Spire Marathon?",
		"25.    In the Dead Adventurer event, the text “…scoured by flames” indicates which Elite?",
		"26.    By what % does Preserved Insect reduce render the size of Elites?",
		"27.    What is the only effect of the Relic Spirit Poop?", "28.    Rest, Smith, Lift, Recall, Toke and ?",
		"29.    Name a relic which was removed from the game during Early Access",
		"30.    What is the default hotkey for End Turn?",
		"31.    What is the name of the popular mod which adds many bosses as playable characters?",
		"32.    What is the current price of Slay the Spire on Steam in USD?",
		"33.    If you have both a Membership card and Smiling Mask, and have not removed any cards yet, what is the price of the Card Removal Service at the merchant?",
		"34.    What color key is gained by Recalling?", "35.    How much damage does Upgraded Grand Finale deal?",
		"36.    On the card Doom and Gloom, is the word “All” written in all caps?",
		"37.    What is the lowest number written in the text of a Starter card?",
		"38.    The beta art for Self Repair features the Defect eating what?",
		"39.    On what floor of the Spire do you encounter the Corrupt Heart?",
		"40.    What is the name of the status effect the Darklings start with?",
		"41.    Name a card with the keyword Fatal", "42.    What is the description text of the keyword Exhaust?",
		"43.    Is the status applied by the card Blasphemy a buff or a debuff?",
		"44.    Name an Ironclad specific potion", "45.    Conjure Blade shuffles which card into your deck?",
		"46.    If you have Envenom in play, and how many times will Bane deal damage to an enemy with no pre applied poison?",
		"47.    Name an X cost Defect card.",
		"48.    What is the requirement for triggering a special sound effect when playing Bowling Bash?",
		"49.    Name one of the menu options presented after entering the Compendium in the main menu.",
		"50.    When browsing your deck in fights, what are the cards sorted by by default?",
		"51.    What color are the flames of the torches in the background of Act 1?",
		"52.    How much damage does Stone Calendar deal?",
		"53.    What is the name of the minions summoned by The Collector?",
		"54.    What is the only boss relic that cannot be gained after the Act 2 boss?",
		"55.    In the “A Note For Yourself” event, what card is offered the first time it is encountered?",
		"56.    What is written on the label of the Merchant’s rug?",
		"57.    If you have 4 Energy, play Swivel and then play Skewer, how many times will Skewer trigger?",
		"58.    What is the maximum amount of Block a character can have?",
		"59.    To become “a boat” what relics do you need?",
		"60.    What is the only card that can be upgraded more than once?",
		"61.    What is the only enemy that can add a permanent card to the player’s deck?",
		"62.    What is the only event that can change the visuals of the background?",
		"63.    How many cards are on offer when you first enter a shop?",
		"64.    What is the meaning of the word “exordium”",
		"65.    In the Joust event, what is the name of the Knight’s pet?"
	};
	
	public static implicit operator TriviaQuestion(string s) => new TriviaQuestion(s);
}