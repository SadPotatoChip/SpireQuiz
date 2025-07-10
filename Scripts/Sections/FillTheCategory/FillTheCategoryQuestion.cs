using System.Collections.Generic;
using System.Linq;

namespace SpireQuiz.Scripts.Sections.FillTheCategory;

public class FillTheCategoryQuestion
{
	public string Text;
	public List<string> Answers;

	public FillTheCategoryQuestion(string text, params string[] answers)
	{
		Text = text;
		Answers = answers.ToList();
	}

	public static List<FillTheCategoryQuestion> AllQuestions = new()
	{
		new("Colorless Card:",
			"Apotheosis", "Apparition", "Bandage Up", "Beta", "Bite", "Blind", "Chrysalis", "Dark Shackles",
			"Deep Breath", "Discovery", "Dramatic Entrance", "Enlightenment", "Expunger", "Finesse", "Flash of Steel",
			"Forethought", "Good Instincts", "Hand of Greed", "Impatience", "Insight", "J.A.X.", "Jack Of All Trades",
			"Madness", "Magnetism", "Master Of Strategy", "Mayhem", "Metamorphosis", "Mind Blast", "Miracle", "Omega",
			"Panacea", "Panache", "Panic Button", "Purity", "Ritual Dagger", "Sadistic Nature", "Safety",
			"Secret Technique", "Secret Weapon", "Shiv", "Smite", "Swift Strike", "The Bomb", "Thinking Ahead",
			"Through Violence", "Transmutation", "Trip", "Violence"),
		new("Class Specific Relic:",
			"Burning Blood", "Ring of the Snake", "Cracked Core", "Pure Water", "Red Skull", "Snecko Skull",
			"Data Disk", "Damaru", "Paper Phrog", "Self-Forming Clay", "Ninja Scroll", "Paper Krane",
			"Gold-Plated Cables",
			"Symbiotic Virus", "Duality", "Teardrop Locket", "Champion Belt", "Magic Flower", "Charon's Ashes",
			"The Specimen",
			"Tough Bandages", "Tingsha", "Emotion Chip", "Cloak Clasp", "Golden Eye", "Brimstone", "Twisted Funnel",
			"Runic Capacitor",
			"Melange", "Black Blood", "Runic Cube", "Mark of Pain", "Ring of the Serpent", "Hovering Kite",
			"Wrist Blade",
			"Frozen Core", "Nuclear Battery", "Inserter", "Holy Water", "Violet Lotus"),
		new("Basic enemy that can appear in only one of the acts (Slime sizes count as 1 enemy)",
			"Acid Slime", "Spike Slime", "Red Louse", "Green Louse", "Darkling", "Spiker", "Orb Walker",
			"Exploder", "Repulsor", "The Maw", "Spire Growth", "Transient", "Writhing Mass", "Byrd",
			"Chosen", "Mugger", "Shelled Parasite", "Centurion", "Mystic", "Snake Plant", "Snecko"
		),
		new("Event Unique to Act 1 or Act 3",
			"Neow", "Big Fish", "The Cleric", "Dead Adventurer", "Golden Idol", "Hypnotizing Colored Mushrooms",
			"Living Wall", "Scrap Ooze", "Shining Light", "The Ssssserpent", "World of Goop", "Wing Statue",
			"Falling Mind Bloom",
			"Mind Bloom", "The Moai Head", "Mysterious Sphere", "Secret Portal", "Sensory Stone",
			"Tomb of Lord Red Mask",
			"Winding Halls"
		)
	};
}