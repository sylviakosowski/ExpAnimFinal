using UnityEngine;
using System.Collections;

public class Dialogue : MonoBehaviour {

	// Variables for actually talking
	private GameObject globObj;
	private Globals globScr;
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

		Debug.LogFormat ("Conversation: {0}", convNum);
	}

	/// <summary>
	/// The functions for triggering the girls responses.
	/// </summary>
	/// <param name="response">Response.</param>

	// Real girlTalk function. Update the global component of what she said and then swap to her response scene.
	void girlTalk (string response) { 
		globScr.updateGirlResp (response);
		Application.LoadLevel ("GirlResp");
		//Application.LoadLevel ("RTC");
	}

	/// <summary>
	/// The memory functions
	/// </summary>

	// We're now checking if memories have been visited before these memory functions, since it didn't require refactoring as much code...
	void memory1 () {
		Globals.memories [0] = true;
		Application.LoadLevel ("opening"); 
	}

	void memory3 () { // We've killed this memory, so we just skip back to where we were.
		Globals.memories [2] = true;
		Application.LoadLevel ("RTC"); 
	}

	void memory4 () {
		Globals.memories [3] = true;
		Application.LoadLevel ("opening"); 
	}

	void memory6 () { // Also killed this one, so we just skip the video and go straight into the conversations.
		// Memory 6 only is revealed from RTC2, and we want to return to the second part of the conversation.
		Globals.convoNumber = 2;
		Globals.convoStage = 1;
		Globals.memories [5] = true;
		Application.LoadLevel ("RTC"); 
	}

	void memory7 () {
		Globals.memories [6] = true;
		Application.LoadLevel ("opening"); 
	}

	void memory8 () {
		Globals.memories [7] = true;
		Application.LoadLevel ("opening");
	}

	void memory9 () {
		Globals.memories [8] = true;
		Application.LoadLevel ("opening"); 
	}

	// TODO: Possibly add functionality to change where you come out of memory 10, right now it always goes to the establishing convo, maybe skip?
	void memory10 () {
		// We want to go back to the second portion of the conversation before continuing on to the next
		// real part of the RTC. That's why we reset the number and stage here. Since any time you come out of memory
		// 10 you'll be going into that part.
		Globals.convoNumber = 0;
		Globals.convoStage = 1;
		Globals.memories [9] = true;
		Application.LoadLevel ("opening"); 
	}

	bool memVisited (int memnumber) {
		return (bool)Globals.memories [memnumber - 1];
	}

	/// <summary>
	/// Functions for the Real Time Conversations - Broken apart for easier management.
	/// </summary>

	// Every conversation needs to either end in a memory or with the girl saying something. This is super important so that the stage will reset.
	void RTC0 () {
		if (convStage == 0) {
			if (GUI.Button ((Rect)rects[0], "If this is about my not supporting you, I really do. I’ve just been exhausted with work. ")) {
				if(memVisited (10) ){
					// Angry
				} else {
					// Go to memory 10, then back to the second half of this conversation. Normally we set the Globals in here, but we do it in
					// memory 10 in this case since every entry to memory 10 will have us back here.
					globScr.updateAnger (-1);
					memory10 (); // Memory 10 then back to stage 2 of conversation (HANDLED IN MEMORY 10)
				}
			}
			if (GUI.Button ((Rect)rects[1], "Have you found someone else?")) {
				if(memVisited (7) ){
					// Angry
				} else {
					Globals.convoNumber = 12; // The Cheating one
					globScr.updateAnger (1);
					memory7 (); // Memory 7
				}
			}
			if (GUI.Button ((Rect)rects[2], "I love you. I don’t know where this is coming from but please, let’s work through this together.")) {
				Globals.convoNumber = 19;
				globScr.updateAnger (-2);
				girlTalk ("You don’t know where this is coming from? Are you sure?"); // She talks and then it goes to the guy
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
					girlTalk ("That’s just it, though. You’re so busy all the time that I feel like I bother you.");
				} else {
					Globals.convoNumber = 2;
					girlTalk ("But that wasn’t the only time...");
				}
			}
			if (GUI.Button ((Rect)rects[1], "I’m truly sorry about that. I want to be there for you.")) {
				Globals.convoStage = 0;
				globScr.updateAnger (-1);
				if (Globals.anger < Globals.angerThresh) {
					Globals.convoNumber = 1;
					girlTalk ("That’s just it, though. You’re so busy all the time that I feel like I bother you.");
				} else {
					Globals.convoNumber = 2;
					girlTalk ("But that wasn’t the only time...");
				}
			}
			if (GUI.Button ((Rect)rects[2], "Maybe...you’re right. I’m too exhausted to give you the marriage you deserve. ")) {
				// End
			} 
		}
	}

	void RTC1 () {
		if (convStage == 0) {
			if (GUI.Button ((Rect)rects[0], "But I still want to support you")) {
				if(memVisited (9) ){
					// Angry
				} else {
					Globals.convoNumber = 4;
					globScr.updateAnger (-1);
					memory9 (); 
				}
			}
			if (GUI.Button ((Rect)rects[1], "I thought I was the one bothering you…")) {
				if(memVisited (9) ){
					// Angry
				} else {
					Globals.convoNumber = 4; 
					globScr.updateAnger (-1);
					memory9 ();
				}
			}
			if (GUI.Button ((Rect)rects[2], "You just don’t see how much I care for you.")) {
				if(memVisited (1) ){
					// Angry
				} else {
					Globals.convoNumber = 10;
					globScr.updateAnger(-1);
					if (Globals.anger < Globals.angerThresh) {
						Globals.conv10Angry = false;
					} else {
						Globals.conv10Angry = true;
					}
					memory1 (); 
				}
			}
			if (GUI.Button ((Rect)rects[3], "You know... I think you might be right. Maybe this is for the best.")) {
				// End
			}
		}
	}

	void RTC2 () { // Also RTC3
		// Girl talked to get us here, now we play the memory, then we come back to the second part of the conversation.
		if (convStage == 0) {
			Globals.convoStage = 1;
			memory6 (); // Dead memory, don't need to check.
		} else if (convStage == 1) {
			Globals.convoStage = 2;
			girlTalk ("Face it, you don't have time for the business and me.");
		} else if (convStage == 2) { // THIS IS ACTUALLY RTC 3
			if (GUI.Button ((Rect)rects[0], "That's not true, you don't have time for me.")) {
				if(memVisited (9) ){
					// Angry
				} else {
					Globals.convoStage = 0;
					Globals.convoNumber = 4;
					globScr.updateAnger (1);
					memory9 (); 
				}
			}
			if (GUI.Button ((Rect)rects[1], "But that's exactly how I feel about you and the theater.")) {
				if(memVisited (9) ){
					// Angry
				} else {
					Globals.convoStage = 0;
					Globals.convoNumber = 4; 
					globScr.updateAnger (-1);
					memory9 (); // Memory 9
				}
			}
			if (GUI.Button ((Rect)rects[2], "This is my dream. You once understood that. But if you can't anymore, maybe it's time we parted")) {
				// End
			}
		}
	}

	void RTC4 () { // Also RTC5
		if (convStage == 0) {
			if (GUI.Button ((Rect)rects [0], "The only person you talk to about things now is him.")) {
				if(memVisited (7) ){
					// Angry
				} else {
					Globals.convoStage = 1;
					globScr.updateAnger (1);
					girlTalk ("Don't be ridiculous!"); // She talks before we play the next memory
				}
			}
			if (GUI.Button ((Rect)rects [1], "You’re so busy that you never include me in your life.")) {
				if(memVisited (8) ){
					// Angry
				} else {
					Globals.convoStage = 2; 
					globScr.updateAnger (1);
					girlTalk ("You were never interested in the first place."); // Again, she talks first.
				}
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
				if(memVisited (8) ){
					// Angry
				} else {
					if (Globals.anger < Globals.angerThresh) {
						Globals.convoNumber = 7;
					} else {
						Globals.convoNumber = 8;
					}
					globScr.updateAnger (-1);
					memory8 ();
				}
			}
			if (GUI.Button ((Rect)rects [1], "Is there something more going on between you?")) {
				Globals.convoNumber = 13; 
				globScr.updateAnger (1);
				girlTalk ("You’re one to talk, what about the waitress you hired to replace me?"); 
			}
			if (GUI.Button ((Rect)rects [2], "We’re like strangers, and I just didn’t see it until now. I’ll sign.")) {
				// End
			}
		}
	}

	void RTC7 () {
		if (convStage == 0) {
			Globals.convoStage = 1;
			girlTalk ("It really hurt me that you didn’t seem to care about my life.");
		} else if (convStage == 1) {
			if (GUI.Button ((Rect)rects [0], "I didn’t know you were so affected by that. I’m sorry.")) {
				Globals.convoStage = 2;
				globScr.updateAnger (-1);
				girlTalk ("I just need more support from you, and I'm not getting it.");
			}
			if (GUI.Button ((Rect)rects [1], "You act like you’re the only one who’s been hurt by something. ")) {
				Globals.convoStage = 2; 
				globScr.updateAnger (1);
				girlTalk ("What do you mean?");
			}
			if (GUI.Button ((Rect)rects [2], "I do care, but I see now that I can’t offer you the support you need. Maybe it’s for the best that we part.")) {
				// End
			}
		} else if (convStage == 2) {
			if (GUI.Button ((Rect)rects [0], "But I’ve tried to give you support, you just haven’t noticed it.")) {
				if(memVisited (1) ){
					// Angry
				} else {
					Globals.convoStage = 0;
					Globals.convoNumber = 10;
					globScr.updateAnger (-1);
					if (Globals.anger < Globals.angerThresh) {
						Globals.conv10Angry = false;
					} else {
						Globals.conv10Angry = true;
					}
					memory1 ();
				}
			}
			if (GUI.Button ((Rect)rects [1], "I do so much for you, but you refuse to notice")) {
				if(memVisited (1) ){
					// Angry
				} else {
					Globals.convoStage = 0;
					Globals.convoNumber = 10;
					globScr.updateAnger (1);
					if (Globals.anger < Globals.angerThresh) {
						Globals.conv10Angry = false;
					} else {
						Globals.conv10Angry = true;
					}
					memory1 ();
				}
			}
			if (GUI.Button ((Rect)rects [2], "I do care, but I see now that I can’t offer you the support you need. Maybe it’s for the best that we part.")) {
				//End
			}
		}
	}

	void RTC8 () {
		if (convStage == 0) {
			Globals.convoStage = 1;
			girlTalk ("You didn't even care about something so important to me.");
		} else if (convStage == 1) {
			if (GUI.Button ((Rect)rects [0], "That's not true, I do care!")) {
				if(memVisited (3) ){
					// Angry
				} else {
					Globals.convoStage = 2;
					globScr.updateAnger (-1);
					girlTalk ("This wasn’t the first time you didn’t make space for me in your life.");
				}
			}
			if (GUI.Button ((Rect)rects [1], "I was busy at the restaurant! Or have you forgotten about that? It used to be important to you too.")) {
				if(memVisited (3) ){
					// Angry
				} else {
					Globals.convoStage = 2; 
					globScr.updateAnger (1);
					girlTalk ("This wasn’t the first time you didn’t make space for me in your life.");
				}
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
			girlTalk ("You always choose your coworkers at the restaurant over me.");
		} else if (convStage == 1) {
			if (GUI.Button ((Rect)rects [0], "Well how do you think I felt when I wanted to surprise you?")) {
				if(memVisited (1) ){
					// Angry
				} else {
					Globals.convoStage = 0;
					Globals.convoNumber = 10;
					globScr.updateAnger (-1);
					if (Globals.anger < Globals.angerThresh) {
						Globals.conv10Angry = false;
					} else {
						Globals.conv10Angry = true;
					}
					memory1 ();
				}
			}
			if (GUI.Button ((Rect)rects [1], "Why are you so resentful about that? I already made plans with my friends that I couldn’t cancel.")) {
				Globals.convoStage = 2; 
				globScr.updateAnger (1);
				girlTalk ("You’ve been pushing me out of your life.");
			}
			if (GUI.Button ((Rect)rects [2], "I need to maintain some sort of independence. And if that bothers you so much, maybe we’d be better off apart.")) {
				// End
			}
		} else if (convStage == 2) {
			if (GUI.Button ((Rect)rects [0], "Why do you think that?")) {
				Globals.convoStage = 0;
				Globals.convoNumber = 13;
				globScr.updateAnger (-1);
				girlTalk ("It all started with the waitress you hired to replace me.");
			}
			if (GUI.Button ((Rect)rects [1], "Same could be said for you. You’ve found someone else, haven't you?")) {
				if(memVisited (7) ){
					// Angry
				} else {
					Globals.convoStage = 0;
					Globals.convoNumber = 12;
					globScr.updateAnger (1);
					memory7 ();
				}
			}
			if (GUI.Button ((Rect)rects [2], "Maybe you’re right. Maybe I need some space.")) {
				// End
			}
		}
	}

	void RTC10 () {
		if (convStage == 0) {
			Globals.convoStage = 1;
			girlTalk ("I appreciate the idea. But would one dinner really have saved this marriage in the long run?");
		} else if (convStage == 1) {
			if (GUI.Button ((Rect)rects [0], "Our marriage can still be saved!")) {
				Globals.convoStage = 2;
				girlTalk ("How?");
			}
			if (GUI.Button ((Rect)rects [1], "I guess not. I guess nothing can save it.")) {
				// End
			}
		} else if (convStage == 2) {
			if (!memVisited (9)) {
				if (GUI.Button ((Rect)rects [0], "You should tell me everything that I am missing from your life. I want to know.")) {
					Globals.convoStage = 0;
					Globals.convoNumber = 4;
					globScr.updateAnger (-1);
					memory9 ();
				}
				if (GUI.Button ((Rect)rects [1], "... I don't know.")) {
					globScr.updateAnger (1);
					Globals.convoStage = 3;
					girlTalk ("Please. I need answers, or I need a signature.");
				}
				if (GUI.Button ((Rect)rects [2], "I guess not. I guess nothing can save it.")) {
					// End
				}
			} else {
				if (GUI.Button ((Rect)rects [0], "... I don't know.")) {
					globScr.updateAnger (1);
					Globals.convoStage = 3;
					girlTalk ("Please. I need answers, or I need a signature.");
				}
				if (GUI.Button ((Rect)rects [1], "I guess not. I guess nothing can save it.")) {
					// End
				}
			}
		} else if (convStage == 3) {
			if (GUI.Button ((Rect)rects [0], "Just give me more time.")) {
				globScr.updateAnger (1);
				Globals.convoStage = 2;
				girlTalk ("I think we've both had enough time. Do you have an solution?");
			}
			if (GUI.Button ((Rect)rects [1], "I guess not. I guess nothing can save it.")) {
				// End
			}
		}
	}

	void RTC10Angry () {
		if (convStage == 0) {
			Globals.convoStage = 1;
			girlTalk ("Do you think a fancy surprise here and there is enough? I want your constant support and I’m not getting it.");
		} else if (convStage == 1) {
			if (GUI.Button ((Rect)rects [0], "Oh and I suppose you’re getting that support from someone else?")) {
				if(memVisited (7) ){
					// Angry
				} else {
					Globals.convoStage = 0;
					Globals.convoNumber = 12;
					globScr.updateAnger (1);
					memory7 ();
				}
			}
			if (GUI.Button ((Rect)rects [1], "But I do support you! I used to go to all your shows when you told me about them.")) {
				if(memVisited (6) ){
					// Angry
				} else {
					Globals.convoStage = 2; 
					girlTalk ("What kind of support is that if your eyes are closed? If you leave early?");
				}
			}
			if (GUI.Button ((Rect)rects [2], "It’s never going to be enough, is it? I quit.")) {
				// End
			}
		} else if (convStage == 2) {
			if(memVisited(10)) {//TODO CHECK THIS STUFF HERE TO MAKE SURE 6 CAN ONLY BE VISITED FROM 10, THEN WE CAN SIMPLIFY THIS.
				// In this case we have visited memory 10.
				Globals.convoStage = 2;
				Globals.convoNumber = 2;
				memory6 ();
			} else {
				memory10 ();
			}
		}
	}

	void RTC12 () {
		if (convStage == 0) {
			if (GUI.Button ((Rect)rects [0], "It’s that guy who stars in all your productions with you, isn’t it?")) {
				Globals.convoNumber = 13;
				girlTalk ("You’re one to talk, what about the waitress you hired to replace me?");
			} 
			if (GUI.Button ((Rect)rects [1], "You know what, I hope you find someone who will make you happy. It's obviously not me.")) {
				// End
			}
		} 
	}

	void RTC13 () { // Also RTC14
		if (convStage == 0) {
			if (GUI.Button ((Rect)rects [0], "What waitress?")) {
				Globals.convoStage = 1;
				globScr.updateAnger (1);
				girlTalk ("... What waitress? What kind of response is that? You hired her yourself!");
			} 
			if (GUI.Button ((Rect)rects [1], "...You’re right. I did hire her to replace you.")) {
				if(memVisited (4) ){
					// Angry
				} else {
					Globals.convoStage = 0;
					if (Globals.anger < Globals.angerThresh) {
						Globals.convoNumber = 15;
					} else {
						Globals.convoNumber = 16;
					}
					globScr.updateAnger (-1);
					memory4 ();
				}
			} 
			if (GUI.Button ((Rect)rects [2], "I can’t believe you don’t trust me. I want out.")) {
				// End
			}
		} else if (convStage == 1) {
			if (GUI.Button ((Rect)rects [0], "I didn’t hire her to replace you.")) {
				Globals.convoStage = 2;
				globScr.updateAnger (1);
				girlTalk ("Are you kidding me? Then why did you ask me to stop working at the restaurant soon after?");
			} 
			if (GUI.Button ((Rect)rects [1], "...You’re right. I did hire her to replace you.")) {
				if(memVisited (4) ){
					// Angry
				} else {
					Globals.convoStage = 0;
					if (Globals.anger < Globals.angerThresh) {
						Globals.convoNumber = 15;
					} else {
						Globals.convoNumber = 16;
					}
					globScr.updateAnger (-1);
					memory4 ();
				}
			} 
			if (GUI.Button ((Rect)rects [2], "I can’t believe you don’t trust me. I want out.")) {
				// End
			}
		} else if (convStage == 2) {
			if (GUI.Button ((Rect)rects [0], "I don’t know what you mean.")) {
				Globals.convoStage = 3;
				globScr.updateAnger (1);
				girlTalk ("I can’t believe this, are you going to be so immature?");
			} 
			if (GUI.Button ((Rect)rects [1], "...You’re right. I did hire her to replace you.")) {
				if(memVisited (4) ){
					// Angry
				} else {
					Globals.convoStage = 0;
					if (Globals.anger < Globals.angerThresh) {
						Globals.convoNumber = 15;
					} else {
						Globals.convoNumber = 16;
					}
					globScr.updateAnger (-1);
					memory4 ();
				}
			} 
			if (GUI.Button ((Rect)rects [2], "I can’t believe you don’t trust me. I want out.")) {
				// End
			}
		} else if (convStage == 3) {
			if (GUI.Button ((Rect)rects [0], "...You’re right. I did hire her to replace you.")) {
				if(memVisited (4) ){
					// Angry
				} else {
					Globals.convoStage = 0;
					if (Globals.anger < Globals.angerThresh) {
						Globals.convoNumber = 15;
					} else {
						Globals.convoNumber = 16;
					}
					globScr.updateAnger (-1);
					memory4 ();
				}
			} 
			if (GUI.Button ((Rect)rects [1], "I can’t believe you don’t trust me. I want out.")) {
				// End
			}
		}
	}

	void RTC15 () {
		if (convStage == 0) {
			globScr.updateAnger (-1);
			Globals.convoStage = 1;
			girlTalk ("I just wish you had talked to me about this before.");
		} else if (convStage == 1) {
			if (GUI.Button ((Rect)rects [0], "I'm sorry. I wanted to but I didn't know how.")) {
				Globals.convoStage = 2;
				globScr.updateAnger (-1);
				girlTalk ("That's not it. We have no trust in this relationship. That's why I never tell you anything anymore.");
			}
			if (GUI.Button ((Rect)rects [1], "Maybe I would have, but you're never around anymore.")) {
				Globals.convoStage = 2; 
				globScr.updateAnger (1);
				girlTalk ("That's not it. We have no trust in this relationship. That's why I never tell you anything anymore.");
			}
		} else if (convStage == 2) {
			if (GUI.Button ((Rect)rects [0], "If this is about me not supporting you, I really do. I've just been exhausted.")) {
				if(memVisited (10) ){
					// Angry
				} else {
					globScr.updateAnger (-1);
					memory10 (); // Again, let memory 10 do the work.
				}
			}
			if (GUI.Button ((Rect)rects [1], "It really hurts me that you never do.")) {
				if(memVisited (9) ){
					// Angry
				} else {
					Globals.convoStage = 0; 
					Globals.convoNumber = 17;
					globScr.updateAnger (1);
					memory9 ();
				}
			}
			if (GUI.Button ((Rect)rects [2], "It’s been a long time since you told me anything about your life. You’re right, I hardly know you anymore,  I’ll sign the papers.")) {
				// End
			}
		}
	}

	void RTC16 () { // Also RTC18
		if (convStage == 0) {
			Globals.convoStage = 1;
			globScr.updateAnger (1);
			girlTalk ("So my life has to cater to yours then? Whenever I have free time, you don't.");
		} else if (convStage == 1) {
			if (GUI.Button ((Rect)rects [0], "It’s almost like you’re intentionally avoiding me.")) {
				if(memVisited (9) ){
					// Angry
				} else {
					Globals.convoStage = 0;
					Globals.convoNumber = 4;
					globScr.updateAnger (1);
					memory9 ();
				}
			}
			if (GUI.Button ((Rect)rects [1], "Maybe if you told me about your plans more we could work things out I hardly know what’s going on in your life anymore?")) {
				if(memVisited (9) ){
					// Angry
				} else {
					Globals.convoStage = 0; 
					Globals.convoNumber = 4;
					globScr.updateAnger (-1);
					memory9 ();
				}
			}
			if (GUI.Button ((Rect)rects [2], "So what if I relax with my friends? I’ve been exhausted with managing the restaurant.")) {
				if(memVisited (10) ){
					// Angry
				} else {
					globScr.updateAnger (1);
					memory10 (); // Again, we let memory10 handle the transitions here.
				}
			}
			if (GUI.Button ((Rect)rects [3], "You want to be so independent? Fine. We're through.")) {
				// End
			}
		}
	}

	void RTC17 () {
		if (convStage == 0) {
			if (GUI.Button ((Rect)rects [0], "... When did you stop counting on me?")) {
				if(memVisited (8) ){
					// Angry
				} else {
					if (Globals.anger < Globals.angerThresh) {
						Globals.convoNumber = 7;
					} else {
						Globals.convoNumber = 8;
					}
					globScr.updateAnger (-1);
					memory8 ();
				}
			}
			if (GUI.Button ((Rect)rects [1], "I don't see why you're acting like this when I've supported you this entire time.")) {
				if(memVisited (8) ){
					// Angry
				} else {
					Globals.convoStage = 1; 
					globScr.updateAnger (1);
					girlTalk ("Have you really?");
				}
			}
			if (GUI.Button ((Rect)rects [2], "You’re right, I hardly know you anymore,  I’ll sign the papers.")) {
				// End
			}
		} else if (convStage == 1) {
			Globals.convoStage = 0;
			if (Globals.anger < Globals.angerThresh) {
				Globals.convoNumber = 7;
			} else {
				Globals.convoNumber = 8;
			}
			memory8 ();
		}
	}

	void RTC19 () {
		if (convStage == 0) {
			// We don't need to check this one because we only get here from the very first conversation and can't have visited memory 9 yet.
			if (GUI.Button ((Rect)rects [0], "All I know is you've stopped confiding in me. And I miss you.")) {
				Globals.convoNumber = 17;
				memory9 ();
			}
		}
	}

	void OnGUI () {
	//void Update () {
		// Make things wrap nicely.
		GUI.skin.button.wordWrap = true;

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
			if (Globals.conv10Angry) {
				RTC10Angry ();
			} else {
				RTC10 ();
			}
		} else if (convNum == 12) {
			RTC12 ();
		} else if (convNum == 13) {
			RTC13 ();
		} else if (convNum == 15) {
			RTC15 ();
		} else if (convNum == 16) {
			RTC16 ();
		} else if (convNum == 17) {
			RTC17 ();
		} else if (convNum == 19) {
			RTC19 ();
		}
	}
}

