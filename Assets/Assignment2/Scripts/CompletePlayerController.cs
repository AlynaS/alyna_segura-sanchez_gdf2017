using UnityEngine;
using System.Collections;

//Adding this allows us to access members of the UI namespace including Text.
using UnityEngine.UI;

public class CompletePlayerController : MonoBehaviour {

	public float speed;				//Floating point variable to store the player's movement speed.
	//public Text countText;			//Store a reference to the UI Text component which will display the number of pickups collected.
	public Text winText;			//Store a reference to the UI Text component which will display the 'You win' message.
	public Text planetText;			//Store a reference to the UI text component which will display the number of planets collected
	public Text alienText;			//Store a reference to the UI text component which will display the number of aliens collected
	public Text scoreText;			//Store a reference to the UI text component which will display the number of total points collected

	private Rigidbody2D rb2d;		//Store a reference to the Rigidbody2D component required to use 2D Physics.
	private int count;				//Integer to store the number of pickups collected so far.
	private int planets;			//Integer to store number of planet pickups collected so far
	private int aliens;				//Integer to store number of alien pichups collected so far
	private int score;

	// Use this for initialization
	void Start()
	{
		//Get and store a reference to the Rigidbody2D component so that we can access it.
		rb2d = GetComponent<Rigidbody2D> ();

		//Initialize count to zero.
		//count = 0;

		//Initialze winText to a blank string since we haven't won yet at beginning.
		winText.text = "";

		planets = 0;	// Initialize number of planets to zero
		aliens = 0;		// Initialize number of aliens to zero
		score = 0;		// Initialize number of points to zero

		//Call our SetCountText function which will update the text with the current value for count.
		//SetCountText ();

		SetPlanetText ();	// Call out SetPlanetText Function which will update the text with the current value for planets
		SetAlienText ();	// Call out SetAlienText Function which will update the text with the current value for aliens
		SetScoreText ();	// Call out SetScoreText Function which will update the text with the current value of the score

	}

	//FixedUpdate is 	called at a fixed interval and is independent of frame rate. Put physics code here.
	void FixedUpdate()
	{
		//Store the current horizontal input in the float moveHorizontal.
		float moveHorizontal = Input.GetAxis ("Horizontal");

		//Store the current vertical input in the float moveVertical.
		float moveVertical = Input.GetAxis ("Vertical");

		//Use the two store floats to create a new Vector2 variable movement.
		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);

		//Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
		rb2d.AddForce (movement * speed);
	}

	//OnTriggerEnter2D is called whenever this object overlaps with a trigger collider.
	void OnTriggerEnter2D(Collider2D other) 
	{
		//Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
		//if (other.gameObject.CompareTag ("PickUp")) 
		//{
			//... then set the other object we just collided with to inactive.
			//other.gameObject.SetActive(false);
			
			//Add one to the current value of our count variable.
			//count = count + 1;
			
			//Update the currently displayed count by calling the SetCountText function.
			//SetCountText ();
		//}

		//Check the provided Collider2D parameter other to see what the player has collided with
		if (other.gameObject.CompareTag ("Planet_PickUp")) { // If the object is a planet...

			other.gameObject.SetActive (false);		// ...then set the other (planet) object we collided with to inactive
			planets += 1;							// Add one to the current value of our planet variable
			score += 5;								// Planet PickUps have a score of 5, so the current score will increase by 5

			SetPlanetText ();						// Updates the planet display counter by calling the SetPlanetText function
			SetScoreText ();						// Updates the score counter by calling the SetScoreText function
		} else if (other.gameObject.CompareTag ("Alien_PickUp")) { //If the object is an alien...

			other.gameObject.SetActive (false);		// ...then set the other (alien) object we collided with to inactive
			aliens += 1;							// Add one to the current value of our alien variable
			score += 2;								// Alien PickUps have a score of 2, so the current score will increase by 5

			SetAlienText ();						// Updates the planet display counter by calling the SetAlienText function
			SetScoreText ();						// Updates the score counter by calling the SetScoreText function
		} else if (other.gameObject.CompareTag ("Laser")) { //If the object is a laser...

			score -= 3;								// Lasers take away 3 points from player's score
			SetScoreText ();						// Updates the score counter by calling the SetScoreText function
		}
		

	}

	//This function updates the text displaying the number of objects we've collected and displays our victory message if we've collected all of them.
	//void SetCountText()
	//{
		//Set the text property of our our countText object to "Count: " followed by the number stored in our count variable.
		//countText.text = "Count: " + count.ToString ();

		//Check if we've collected all 12 pickups. If we have...
		//if (count >= 12)
			//... then set the text property of our winText object to "You win!"
			//winText.text = "You win!";
	//}


	//This function will update the text displaying the numbre of planets the player has collected
	void SetPlanetText()
	{
		//Set the text of planetText object to "Planets: " followed by number of planets picked up
		planetText.text = "Planets: " + planets.ToString ();	

	}

	// This function will update the text displaying the number of aliens the player has collected
	void SetAlienText()
	{
		//Set the text of alienText object to "Planets: " followed by number of aliens picked up
		alienText.text = "Aliens: " + aliens.ToString ();

	}

	// This function will update the text displaying the number of points the player has collected
	void SetScoreText()
	{
		//Set the text of planetText object to "Score: " followed by number of points collected
		scoreText.text = "Score: " + score.ToString ();

		// Telling the player they won if they get 15 points
		if (score >= 15) {
			winText.text = "You Win!";
		}
	}
}
