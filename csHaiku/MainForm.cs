using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace csHaiku {
	public partial class MainForm : Form {
		public MainForm() {
			InitializeComponent();
		}

       	//Initialising variables
        string nounInput1 = "";
		string nounInput2 = "";
		string adjectiveInput = "";
		string verbInput = "";
		string[] inputtedWords = new string[5];
		int[] numVowels = new int[5];

		List<string[]> fillWordsList = new List<string[]> {
	    	new string[10] {"I", "you", "we", "they", "she", "he", "time", "God", "happines", "sadnes"}, //Pronoun
			new string[8] {"hate", "coldness", "heart", "faith", "skin", "flight", "grave", "world"}, //Noun
			new string[8] {"water", "earth", "soil", "clouds", "emptiness", "space", "cavern", "shadows"}, //Place
			new string[8] {"in", "into", "between", "through", "past", "across", "near", "beneath"}, //Preposition
			new string[8] {"a", "the", "my", "our", "his", "her", "their", "your"}, //Article
			new string[8] {"slither", "crawl", "creep", "dive", "stroll", "push", "drift", "paddle"}, //verbAction
			new string[8] {"gaze", "listen", "stare", "ignore", "weep", "shudder", "fall", "smile"}, //verbNoAction
			new string[8] {"cold", "wet", "slow", "torpid", "silent", "harsh", "misty", "senseless"}, //adjective
		};
		
		string line1 = "";
		string line2 = "";
		string line3 = "";
		
		Random rng = new Random();
		int randomNum = 0;

		/// <summary>
		/// When a text field is left empty, 
		/// The variables are just set to a random from the existing arrays
		/// Set the right array element to equal the outcome of the pre-existing words
		/// Else,
		/// Set the right array element to equal the outcome of the inputs
		/// </summary>
		void inputChecks() {
			inputtedWords[1] = nounInput1;
			inputtedWords[2] = nounInput2;
			inputtedWords[3] = adjectiveInput;
			inputtedWords[4] = verbInput;

            if (nounInput1 == "") {
				randomNum = rng.Next(0, 8);
				inputtedWords[1] = fillWordsList[2][randomNum];
            }
			if (nounInput2 == "") {
				randomNum = rng.Next(0, 8);
				inputtedWords[2] = fillWordsList[2][randomNum];
			}
			if (adjectiveInput == "") {
				randomNum = rng.Next(0, 8);
				inputtedWords[3] = fillWordsList[7][randomNum];
			}
			if (verbInput ==  "") {
				randomNum = rng.Next(0, 8);
				inputtedWords[4] = fillWordsList[6][randomNum];
			}
			
			syllableCounter();
		}
		
		/// <summary>
		/// First, the program loops through the algorithm for each word inputted, as shows in the inputtedWords array
		/// Then each character is checked through in currentword, 
		/// 	-Each vowel is checked through
		/// 	-If the current char is not a vowel, do not do anything (other than changing some bools for the algorithm)
		/// 	-If the current char is a vowel, add 1 to the numberofvowels found
		/// 	-If a vowel has not been found, set lastWasVowel to false so that the algorithm can continue to repeat
		/// 	-Just to remove some silent "es" and "e"s, some checking from the end of the word is done so that the vowel "e" can be removed as it is counted as silent
		/// </summary>
        void syllableCounter() {
			for (int i = 1; i < 5; i++) {
				char[] vowels = {'a', 'e', 'i', 'o', 'u'};
				numVowels[i] = 0;
				bool lastWasVowel = false;
					
				foreach (char wc in inputtedWords[i]) {
					bool foundVowel = false;
					foreach (char v in vowels) {
						if (v == wc && lastWasVowel) {
							foundVowel = true;
							lastWasVowel = true;
							break;
						} else if (v == wc && !lastWasVowel) {
							numVowels[i]++;
							foundVowel = true;
							lastWasVowel = true;
							break;
						}
					}
					if (!foundVowel) {
						lastWasVowel = false;
					}
				}
				
				//Removing possibility of silent "es"
				if (inputtedWords[i].Length > 2 && inputtedWords[i].Substring(inputtedWords[i].Length - 2) == "es") {
					numVowels[i]--;
				//Removing possibility of silent "e"
				} else if (inputtedWords[i].Length > 1 && inputtedWords[i].Substring(inputtedWords[i].Length - 1) == "e") {
					numVowels[i]--;
				}
				
				switch (i) {
					case 1:
						if (numVowels[i] > 2) {
							inputtedWords[i] = fillWordsList[1][randomNum];
						}
						break;
					case 2:
						if (numVowels[i] > 2) {
							inputtedWords[i] = fillWordsList[1][randomNum];
						}
						break;
					case 3:
						if (numVowels[i] > 3) {
							inputtedWords[i] = fillWordsList[7][randomNum];
						}
						break;
					case 4:
						if (numVowels[i] > 4) {
							inputtedWords[i] = fillWordsList[5][randomNum];
						}
						break;
				}
			}
		}
        
		/// <summary>
		/// If the first noun's last character is an "s",
		/// Add an "s" to the verb
		/// Else,
		///     If the verb already has an "s" on its end,
		///     Set its end to "ed" because the noun doesn't have "s" at the end
		///     Else,
		///     Still add an "s" onto the end of the verb
		/// </summary>
		void grammarChecks() {
			char[] trimChar = {'s'};
			if (inputtedWords[1].Substring(inputtedWords[1].Length -1, 1) == "s") {
				if (inputtedWords[4].Substring(inputtedWords[4].Length -1, 1) == "s") {
					inputtedWords[4] = inputtedWords[4] + "";
				} else {
					inputtedWords[4] = inputtedWords[4] + "s";
				}
			} else {
				if (inputtedWords[4].Substring(inputtedWords[4].Length -1, 1) == "s") {
					inputtedWords[4] = inputtedWords[4].TrimEnd(trimChar) + "ed";
				} else {
					inputtedWords[4] = inputtedWords[4] + "s";
				}
			}
		}

		/// <summary>
		/// The first line is simple 
		///     -Add the randomised words from existing arrays to each other
		/// </summary>
        void line1Build() {
			randomNum = rng.Next(0, 8);
			line1 = fillWordsList[4][randomNum] + " ";
			
			randomNum = rng.Next(0, 8);
			line1 = line1 + fillWordsList[7][randomNum] + " " + inputtedWords[1] + " " + inputtedWords[4];
		}

		/// <summary>
		/// The second line is slightly more complex
		///     -The first word is a pre-existing preposition
		///     -The second word is an pre-existing fillWordsList[5]
		///     -Third word is the inputted adjective
		///     -Final word is the inputted noun
		/// </summary>
        void line2Build() {
			randomNum = rng.Next(0, 8);
			line2 = fillWordsList[3][randomNum];
			
			randomNum = rng.Next(0, 8);
			line2 = line2 + " " + fillWordsList[4][randomNum] + " " + inputtedWords[3] + " " + inputtedWords[2] + ", ";
		}
        
		/// <summary>
		/// Third line is FAR more complex
		///     If the pre-existing ajective (randomly generated) ends with a "y"
		///     Add an "ily" to the end of it
		///     Else,
		///     Add an "ly"
		///     
		///     Ranomising the . and , for the second word end
		///		     
		///     Adding a pre-existing pronoun to the third line
		///     If one of the pronouns is in the second subcatergory
		/// 	Add an "s" onto the end
		/// 
		/// 	If one of the pre-existing verbs is in the second subcatergory
		/// 	Add it to the line
		/// 	Else,
		/// 	If there is an "s" on the end of the word
		/// 		Add the word to the line
		/// 		Else
		/// 		If there is no "s" on the end of the word
		/// 		Add an "s" onto the end, and then add it
		/// 		
		/// </summary>
        void line3Build() {
	        //FIRST WORD
			randomNum = rng.Next(0, 8);
			if (fillWordsList[7][randomNum].Substring(fillWordsList[7][randomNum].Length -1, 1) == "y") {
				char[] trimChars = {'y'};
				line3 = (fillWordsList[7][randomNum].TrimEnd(trimChars) + "ily");
			} else {
				line3 = (fillWordsList[7][randomNum].TrimEnd() + "ly");
			}
			
			//COMMA OR FULL STOP SWITCHER
			if (rng.Next(0, 3) > 1) {
				line3 = line3 + ".";
			} else {
				line3 = line3 + ",";
			}
			
			//SECOND WORD
			randomNum = rng.Next(0, 10);
			line3 = line3 + " " + fillWordsList[0][randomNum];
			if (randomNum > 6) {
				line3 = line3 + "s";
			}
			
			//THIRD WORD
			randomNum = rng.Next(0, 8);
			if (randomNum < 4) {
				//TODO: Find a way to incoportate if the pronoun is in second subcategory (aka God+), make sure the verbnoaction does have an "s" added to it, if not then dont
				line3 = line3 + " " + fillWordsList[6][randomNum];
			} else {
				if (fillWordsList[6][randomNum].Substring(fillWordsList[6][randomNum].Length - 1) == "s") {
					MessageBox.Show("Last word does have s at the end");
					line3 = line3 + " " + fillWordsList[6][randomNum];
				} else {
					char[] trimChar2 = {'s'};
					line3 = line3 + " " + fillWordsList[6][randomNum].TrimEnd(trimChar2);
				}
			}
        }
		
		void BtnSubmitClick(object sender, EventArgs e) {
			line1 = "";
			line2 = "";
			line3 = "";
			
			nounInput1 = txtNoun1.Text;
			nounInput2 = txtNoun2.Text;
			adjectiveInput = txtAdjective.Text;
			verbInput = txtVerb.Text;
		
			inputChecks();
			grammarChecks();
			line1Build();
			line2Build();
			line3Build();
			
			lblOutput1.Text = line1;
			lblOutput2.Text = line2;
			lblOutput3.Text = line3;
		}
		
		void BtnExampleClick(object sender, EventArgs e) {
			txtNoun1.Text = "table";
			txtNoun2.Text = "chair";
			txtVerb.Text = "sit";
			txtAdjective.Text = "big";
		}
		
		void BtnGenerateEx2Click(object sender, EventArgs e) {
			txtNoun1.Text = "shmeememus";
			txtNoun2.Text = "regulator";
			txtVerb.Text = "superduperyla";
			txtAdjective.Text = "enourmous";			
		}
	}
}