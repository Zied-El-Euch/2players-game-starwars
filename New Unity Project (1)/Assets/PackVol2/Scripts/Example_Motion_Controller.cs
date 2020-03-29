using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Example Script for motion (Walk, jump and dying), for dying press 'k'...
public class Example_Motion_Controller : MonoBehaviour {
	public GameObject BulletPrefab;
	public Text healthText;
	private float maxspeed; //walk speed
	Animator anim;
	private bool faceright; //face side of sprite activated
	public bool jumping=false;
	private bool isdead=false;
	public string Axis;
	public bool MouseAttack = false;
	public string JumpAxis = "Jump";
	public int health = 3;
	public int speed = 200;
	public float AttackingTime = 0.0f;
	public float AttackRequiredTime = 1f;
	public GameObject Panel;
	public GameObject Panel1;
	public Text winner;
	public GameObject ReplayButton;

	//--
	void Start () {
		maxspeed=2f;//Set walk speed
		faceright=true;//Default right side
		anim = this.gameObject.GetComponent<Animator> ();
		anim.SetBool ("walk", false);//Walking animation is deactivated
		anim.SetBool ("dead", false);//Dying animation is deactivated
		anim.SetBool ("jump", false);//Jumping animation is deactivated
		ReplayButton.SetActive(false);
	}
	
	void OnCollisionEnter2D(Collision2D coll) {
		//if (coll.gameObject.tag == "Ground"){//################Important, the floor Tag must be "Ground" to detect the collision!!!!
			jumping=false;
			anim.SetBool ("jump", false);
		if (coll.gameObject.tag == "jumper")
		{
			GetComponent<Rigidbody2D>().AddForce(Vector2.up * 50f, ForceMode2D.Impulse);
		}
		if (coll.gameObject.tag == "trap")
		{
			HealthDecrease();
		}
		//}
		if (coll.gameObject.tag == "batta")
		{
			health = 0;
			healthText.text = "Health: " + health;
			Destroy(gameObject);
			Time.timeScale = 0;
			ReplayButton.SetActive(true);
			if (gameObject.name == "Player2")
			{
				winner.text = "player1 won";
			}
			else
			{
				winner.text = "player2 won";
			}
		}

	}

	public void HealthDecrease()
	{
		health--;
		healthText.text = "Health: " + health;
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "objective")
		{
			Time.timeScale = 0;
			ReplayButton.SetActive(true);
		}
		if (coll.gameObject.tag == "ground")
		{
			jumping = true;
		}
		/*else
		{
			jumping = false;
		}*/
		
	}
	void Update () {
		if(isdead==false){
			//--DYING
			if(health== 0){//###########Change the dead event, for example: life bar=0
				anim.SetBool ("dead", true);
				isdead=true;
				
			}
			//--END DYING
			if ((MouseAttack && Input.GetMouseButtonDown(0)) || (Input.GetKeyDown(KeyCode.S) && !MouseAttack))
			{
				if (AttackingTime >= AttackRequiredTime)
				{
					GameObject bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
					if (faceright)
						bullet.GetComponent<Bullet>().direction = transform.right;
					else bullet.GetComponent<Bullet>().direction = -transform.right;
					bullet.GetComponent<Bullet>().Owner = "Player";
					AttackingTime = 0;
				}
			}
			else
			{
				if (AttackingTime < AttackRequiredTime)
					AttackingTime += Time.deltaTime;
			}
			//--JUMPING
			if (Input.GetButtonDown(JumpAxis)){
				if(jumping==false){//only once time each jump
					GetComponent<Rigidbody2D>().AddForce(new Vector2(100,500));
					jumping=true;
					anim.SetBool ("jump", true);
				}
			}
			//--END JUMPING

			

			//--WALKING
			float move = Input.GetAxis (Axis);
			GetComponent<Rigidbody2D>().velocity = new Vector2(move * speed, GetComponent<Rigidbody2D>().velocity.y);
			if(move>0){//Go right
				anim.SetBool ("walk", true);//Walking animation is activated
				if(faceright==false){
					Flip ();
				}
				Panel.SetActive(false);
				Panel1.SetActive(false);

			}
			if(move==0){//Stop
				anim.SetBool ("walk", false);
			}			
			if((move<0)){//Go left
				anim.SetBool ("walk", true);
				if(faceright==true){
					Flip ();
				}
				Panel.SetActive(false);
				Panel1.SetActive(false);
			}
			//END WALKING
			

		}
		
		
	}

	public void OnDeath()
	{
		if (gameObject.name == "Player2")
		{
			winner.text="player1 won";
		}
		else
		{
			winner.text="player2 won";
		}
		Time.timeScale = 0;
		ReplayButton.SetActive(true);


	}
	public void Replay()
	{
		//This line changes the scene to the Scene 0 in your build settings
		SceneManager.LoadScene(0);
		Time.timeScale = 1;
	}


	void Flip(){
		faceright=!faceright;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	
	
	
}
