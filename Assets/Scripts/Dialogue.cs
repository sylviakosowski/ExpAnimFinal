using UnityEngine;
using System.Collections;

public class Dialogue : MonoBehaviour {

	// Variables for actually talking
	private GameObject globObj;
	private Globals globScr;
	private bool isTalking;
	private int convNum;
	private int convStage;

	private ArrayList memories;

	// Variables for drawing
	private int cX;
	private int cY;

	private ArrayList rects;

	/// <summary>
	/// ACTUALLY IMPLEMENT A STYLE
	/// </summary>



	// Use this for initialization
	void Start () {
		isTalking = false;
		cX = Screen.width / 2;
		cY = Screen.height / 2;

		globObj = GameObject.Find ("GlobalObj");
		globScr = globObj.GetComponent<Globals> ();
		convNum = Globals.convoNumber;

		convStage = Globals.convoStage;

		rects = new ArrayList ();
		rects.Add (new Rect (cX - 150, cY + 10, 300, 60));
		rects.Add (new Rect (cX - 150, cY + 70, 300, 60));
		rects.Add (new Rect (cX - 150, cY + 130, 300, 60));
		rects.Add (new Rect (cX - 150, cY + 190, 300, 60));

		memories = Globals.memories;

		Debug.Log (convNum.ToString());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void girlTalk () { // Dummy girl talking function, update to actually do what we need to later.
		Application.LoadLevel ("RTC");
	}

	void memory1 () {
		if ((bool)memories [0]) {
			// Angry response
		} else {
			Globals.memories [0] = true;
			Application.LoadLevel ("opening"); 
		}
	}

	void memory3 () {
		if ((bool)memories [2]) {
			// Angry response
		} else {
			Globals.memories [2] = true;
			Application.LoadLevel ("opening"); 
		}
	}

	void memory6 () {
		if ((bool)memories [5]) {
			// Angry response
		} else {
			// Memory 6 only is revealed from RTC2, and we want to return to the second part of the conversation.
			Globals.convoNumber = 2;
			Globals.convoStage = 1;
			Globals.memories [5] = true;
			Application.LoadLevel ("opening"); 
		}
	}

	void memory7 () {
		if ((bool)memories [6]) {
			// Angry response
		} else {
			Globals.memories [6] = true;
			Application.LoadLevel ("opening"); 
		}
	}

	void memory8 () {
		if ((bool)memories [7]) {
			// Angry response
		} else {
			Globals.memories [7] = true;
			Application.LoadLevel ("opening"); 
		}
	}

	void memory9 () {
		if ((bool)memories [8]) {
			// Angry response and loop back to current conversation
		} else {
			Globals.memories [8] = true;
			Application.LoadLevel ("opening"); 
		}
	}

	void memory10 () {
		if ((bool)memories [9]) {
			// Angry response
		} else {
			// We want to go back to the second portion of the conversation before continuing on to the next
			// real part of the RTC. That's why we reset the number and stage here. Since any time you come out of memory
			// 10 you'll be going into that part.
			Globals.convoNumber = 0;
			Globals.convoStage = 1;
			Globals.memories [9] = true;
			Application.LoadLevel ("opening"); 
		}
	}

	// TODO - GIRL PARTS WILL ALLOW THIS TO TRANSITION
	// Every conversation needs to either end in a memory or with the girl saying something. This is super important so that the stage will reset.
	void RTC0 () {
		if (convStage == 0) {
			if (GUI.Button ((Rect)rects[0], "If this is about my not supporting you, I really do. I’ve just been exhausted with work. ")) {
				// Go to memory 10, then back to the second half of this conversation. Normally we set the Globals in here, but we do it in
				// memory 10 in this case since every entry to memory 10 will have us back here.
				globScr.updateAnger (-1);
				memory10 (); // Memory 10 then back to stage 2 of conversation (HANDLED IN MEMORY 10)
			}
			if (GUI.Button ((Rect)rects[1], "Have you found someone else?")) {
				Globals.convoNumber = 12; // The Cheating one
				globScr.updateAnger (1);
				memory7 (); // Memory 7
			}
			if (GUI.Button ((Rect)rects[2], "I love you. I don’t know where this is coming from but please, let’s work through this together.")) {
				Globals.convoNumber = 19;
				globScr.updateAnger (-2);
				memory9(); // Memory 9
			} 
			if (GUI.Button ((Rect)rects[3], "...Okay. Frankly, I’m relieved. ")) {
				// End
			}
		} else if (convStage == 1) {
			if (GUI.Button ((Rect)rects[0], "Isn’t it enough that I showed my support by being there? ")) {
				Globals.convoStage = 0; 
				globScr.updateAnger (1);
				if (Globals.anger < Globals.angerThresh) {
					Globals.convoNumber = 1;
				} else {
					Globals.convoNumber = 2;
				}
				girlTalk ();
			}
			if (GUI.Button ((Rect)rects[1], "I’m truly sorry about that. I want to be there for you.")) {
				Globals.convoStage = 0;
				globScr.updateAnger (-1);
				if (Globals.anger < Globals.angerThresh) {
					Globals.convoNumber = 1;
				} else {
					Globals.convoNumber = 2;
				}
				girlTalk ();
			}
			if (GUI.Button ((Rect)rects[2], "Maybe...you’re right. I’m too exhausted to give you the marriage you deserve. ")) {
				// End
			} 
		}
	}

	void RTC1 () {
		if (convStage == 0) {
			if (GUI.Button ((Rect)rects[0], "But I still want to support you")) {
				Globals.convoNumber = 4;
				globScr.updateAnger (-1);
				memory9 (); 
			}
			if (GUI.Button ((Rect)rects[1], "I thought I was the one bothering you…")) {
				Globals.convoNumber = 4; 
				globScr.updateAnger (-1);
				memory9 (); // Memory 7
			}
			if (GUI.Button ((Rect)rects[2], "You just don’t see how much I care for you.")) {
				Globals.convoNumber = 1; // TODO: UPDATE THIS TO BE CORRECT
				globScr.updateAnger(-1);
				memory1 (); 
			}

			if (GUI.Button ((Rect)rects[3], "You know... I think you might be right. Maybe this is for the best.")) {
				// End
			}
		}
	}

	void RTC2 () { // Also RTC3
		// Girl talks, then we play the memory, then we come back to the second part of the conversation.
		if (convStage == 0) {
			Globals.convoStage = 1;
			girlTalk ();
		} else if (convStage == 1) {
			Globals.convoStage = 2;
			memory6 ();
		} else if (convStage == 2) {
			Globals.convoStage = 3;
			girlTalk ();
		} else if (convStage == 3) { // THIS IS ACTUALLY RTC 3
			if (GUI.Button ((Rect)rects[0], "That's not true, you don't have time for me.")) {
				Globals.convoStage = 0;
				Globals.convoNumber = 4;
				globScr.updateAnger (1);
				memory9 (); 
			}
			if (GUI.Button ((Rect)rects[1], "But that's exactly how I feel about you and the theater.")) {
				Globals.convoStage = 0;
				Globals.convoNumber = 4; 
				globScr.updateAnger (-1);
				memory9 (); // Memory 9
			}
			if (GUI.Button ((Rect)rects[2], "This is my dream. You once understood that. But if you can't anymore, maybe it's time we parted")) {
				// End
			}
		}
	}

	void RTC4 () { // Also RTC5
		if (convStage == 0) {
			if (GUI.Button ((Rect)rects [0], "The only person you talk to about things now is him.")) {
				Globals.convoStage = 1;
				globScr.updateAnger (1);
				girlTalk (); // She talks before we play the next memory
			}
			if (GUI.Button ((Rect)rects [1], "You’re so busy that you never include me in your life.")) {
				Globals.convoStage = 2; 
				globScr.updateAnger (1);
				girlTalk (); // Again, she talks first.
			}
			if (GUI.Button ((Rect)rects [2], "You no longer care about having me in your life. I'll sign.")) {
				// End
			}
		} else if (convStage == 1) {
			Globals.convoStage = 0;
			Globals.convoNumber = 6;
			memory7 ();
		} else if (convStage == 2) {
			Globals.convoStage = 0;
			if (Globals.anger < Globals.angerThresh) {
				Globals.convoNumber = 7;
			} else {
				Globals.convoNumber = 8;
			}
			memory8 ();
		}
	}

	void RTC6 () {
		if (convStage == 0) {
			if (GUI.Button ((Rect)rects [0], "Why don’t you feel like you can confide in me anymore? ")) {
				if (Globals.anger < Globals.angerThresh) {
					Globals.convoNumber = 7;
				} else {
					Globals.convoNumber = 8;
				}
				globScr.updateAnger (-1);
				memory8 ();
			}
			if (GUI.Button ((Rect)rects [1], "Is there something more going on between you?")) {
				Globals.convoNumber = 12; // TODO: Clarify how this fits in exactly.
				globScr.updateAnger (1);
				girlTalk (); // I believe that this fits in because she talks after he says something and then you continue down the RTC 12 branch
			}
			if (GUI.Button ((Rect)rects [2], "We’re like strangers, and I just didn’t see it until now. I’ll sign.")) {
				// End
			}
		}
	}

	void RTC7 () {
		if (convStage == 0) {
			Globals.convoStage = 1;
			girlTalk ();
		} else if (convStage == 1) {
			if (GUI.Button ((Rect)rects [0], "I didn’t know you were so affected by that. I’m sorry.")) {
				Globals.convoStage = 2;
				globScr.updateAnger (-1);
				girlTalk ();
			}
			if (GUI.Button ((Rect)rects [1], "You act like you’re the only one who’s been hurt by something. ")) {
				Globals.convoStage = 2; 
				globScr.updateAnger (1);
				girlTalk ();
			}
			if (GUI.Button ((Rect)rects [2], "I do care, but I see now that I can’t offer you the support you need. Maybe it’s for the best that we part.")) {
				// End
			}
		} else if (convStage == 2) {
			if (GUI.Button ((Rect)rects [0], "But I’ve tried to give you support, you just haven’t noticed it.")) {
				Globals.convoStage = 0;
				Globals.convoNumber = 10;
				globScr.updateAnger (-1);
				memory1 ();
			}
			if (GUI.Button ((Rect)rects [1], "I do so much for you, but you refuse to notice")) {
				Globals.convoStage = 0;
				Globals.convoNumber = 10;
				globScr.updateAnger (1);
				memory1 ();
			}
			if (GUI.Button ((Rect)rects [2], "I do care, but I see now that I can’t offer you the support you need. Maybe it’s for the best that we part.")) {
				//End
			}
		}
	}

	void RTC8 () {
		if (convStage == 0) {
			Globals.convoStage = 1;
			girlTalk ();
		} else if (convStage == 1) {
			if (GUI.Button ((Rect)rects [0], "That's not true, I do care!")) {
				Globals.convoStage = 2;
				globScr.updateAnger (-1);
				girlTalk ();
			}
			if (GUI.Button ((Rect)rects [1], "I was busy at the restaurant! Or have you forgotten about that? It used to be important to you too.")) {
				Globals.convoStage = 2; 
				globScr.updateAnger (1);
				girlTalk ();
			}
			if (GUI.Button ((Rect)rects [2], "I do care, you just refuse to see that. Maybe we should separate.")) {
				// End
			}
		} else if (convStage == 2) {
			Globals.convoNumber = 9;
			Globals.convoStage = 0;
			memory3 ();
		}
	}

	void RTC9 () { // Also RTC11
		if (convStage == 0) {
			Globals.convoStage = 1;
			girlTalk ();
		} else if (convStage == 1) {
			if (GUI.Button ((Rect)rects [0], "Well how do you think I felt when I wanted to surprise you?")) {
				Globals.convoStage = 0;
				Globals.convoNumber = 10;
				globScr.updateAnger (-1);
				memory1 ();
			}
			if (GUI.Button ((Rect)rects [1], "Why are you so resentful about that? I already made plans with my friends that I couldn’t cancel.")) {
				Globals.convoStage = 2; 
				globScr.updateAnger (1);
				girlTalk ();
			}
			if (GUI.Button ((Rect)rects [2], "I need to maintain some sort of independence. And if that bothers you so much, maybe we’d be better off apart.")) {
				// End
			}
		} else if (convStage == 2) {
			if (GUI.Button ((Rect)rects [0], "Why do you think that?")) {
				Globals.convoStage = 0;
				Globals.convoNumber = 10; // TODO: Fix this to be the correct new conversation pick up.
				globScr.updateAnger (-1);
				girlTalk ();
			}
			if (GUI.Button ((Rect)rects [1], "Same could be said for you. You’re replacing me with your co-star at the theater.")) {
				Globals.convoStage = 0;
				Globals.convoNumber = 12;
				globScr.updateAnger (1);
				memory7 ();
			}
			if (GUI.Button ((Rect)rects [2], "Maybe you’re right. Maybe I need some space.")) {
				// End
			}
		}
	}

	void RTC10 () {
		if (convStage == 0) {
			Globals.convoStage = 1;
			girlTalk ();
		} else if (convStage == 1) {
			if (GUI.Button ((Rect)rects [0], "Our marriage can still be saved!")) {
				Globals.convoStage = 2;
				girlTalk ();
			}
			if (GUI.Button ((Rect)rects [1], "I guess not. I guess nothing can save it.")) {
				// End
			}
		} else if (convStage == 2) {
			if (!(bool)memories [8]) {
				if (GUI.Button ((Rect)rects [0], "You should tell me everything that I am missing from your life. I want to know.")) {
					Globals.convoStage = 0;
					Globals.convoNumber = 4;
					globScr.updateAnger (-1);
					memory9 ();
				}
				if (GUI.Button ((Rect)rects [1], "... I don't know.")) {
					globScr.updateAnger (1);
					Globals.convoStage = 3;
					girlTalk ();
				}
				if (GUI.Button ((Rect)rects [2], "I guess not. I guess nothing can save it.")) {
					// End
				}
			} else {
				if (GUI.Button ((Rect)rects [0], "... I don't know.")) {
					globScr.updateAnger (1);
					Globals.convoStage = 3;
					girlTalk ();
				}
				if (GUI.Button ((Rect)rects [1], "I guess not. I guess nothing can save it.")) {
					// End
				}
			}
		} else if (convStage == 3) {
			if (GUI.Button ((Rect)rects [0], "Just give me more time.")) {
				globScr.updateAnger (1);
				Globals.convoStage = 2;
				girlTalk ();
			}
			if (GUI.Button ((Rect)rects [1], "I guess not. I guess nothing can save it.")) {
				// End
			}
		}
	}

	void RTC10Angry () {
		if (convStage == 0) {
			Globals.convoStage = 1;
			girlTalk ();
		} else if (convStage == 1) {
			if (GUI.Button ((Rect)rects [0], "Oh and I suppose you’re getting that support from your co-star?")) {
				Globals.convoStage = 0;
				Globals.convoNumber = 12;
				globScr.updateAnger (1);
				memory7 ();
			}
			if (GUI.Button ((Rect)rects [1], "But I do support you! I used to go to all your shows when you told me about them.")) {
				Globals.convoStage = 2; 
				girlTalk ();
			}
			if (GUI.Button ((Rect)rects [2], "It’s never going to be enough, is it? I quit.")) {
				// End
			}
		} else if (convStage == 2) {
			if((bool)memories[9]) {
				// In this case we have visited memory 10.
				Globals.convoStage = 2;
				Globals.convoNumber = 2;
				memory6 ();
			} else {
				memory10 ();
			}
		}
	}

	void OnGUI () {
		if (convNum == 0) {
			RTC0 ();
		} else if (convNum == 1) {
			RTC1 ();
		} else if (convNum == 2) {
			RTC2 ();
		} else if (convNum == 4) { // We skip 3 because 3 is actually rolled into 2. Whoops.
			RTC4 ();
		} else if (convNum == 6) {
			RTC6 ();
		} else if (convNum == 7) {
			RTC7 ();
		} else if (convNum == 8) {
			RTC8 ();
		} else if (convNum == 9) {
			RTC9 ();
		} else if (convNum == 10) {
			if (Globals.conv10Angry == 0) {
				if (Globals.anger < Globals.angerThresh) {
					Globals.conv10Angry = 1;
					RTC10 ();
				} else {
					Globals.conv10Angry = 2;
					RTC10Angry ();
				}
			} else {
				if (Globals.conv10Angry == 1) {
					RTC10 ();
				} else {
					RTC10Angry ();
				}
			}
		} 


	}
}
























