using UnityEngine;
using System.Collections;
public class PlayerMovement : MonoBehaviour
{
    private PlayerMovement otherEgg;
    private int id;
    private int amountOfPlayers; // initialize to how many players there are in each game
    public bool bounced;
    private bool[] isDead; // initialize to how many players there are in each game is false
    private Vector3 velocity;
    private Vector3 friction;
    private float accelRate = .055f;
    private float boostRate = .09f;
    private float accel;
    public int joystickNum;
    private float mu = 0.037f;
    private float currFrict;
    private float gravity = 10f; // was 13
    private float jumpPower = 6f; // was 15
    private float knockPower = 1.2f; // was 1.25f
    private float boost = 1.8f;
    private bool fallen;

    private float boostCool = 0;

    private Rigidbody eggBod;

    void Start()
    {
        switch(this.name)
        {
            case "egg1": id = 1; break;
            case "egg2": id = 2; break;
            case "egg3": id = 3; break;
            case "egg4": id = 4; break;
			case "egg5": id = 5; break;
			case "egg6": id = 6; break;
            default: id = 7; break;
        }
        eggBod = GetComponent<Rigidbody>();
		//Debug.Log (eggBod.centerOfMass);
		Vector3 newCenter = eggBod.centerOfMass;
		newCenter.y *= .2f;
		eggBod.centerOfMass = newCenter;
        accel = accelRate;
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        CalculateForces();
    }

    void CalculateForces ()
    {
        string joystickString = joystickNum.ToString();

        Vector3 acceleration = new Vector3();

        if (Input.GetButtonDown("A_" + joystickString) && !fallen)
        {
            acceleration.y += jumpPower;
            fallen = true;
            currFrict = 0;
        }
        else if (fallen)
            acceleration.y -= gravity * Time.deltaTime;
        else
        {
            velocity.y = 0;
            acceleration.y = 0;
        }

        acceleration.x += Input.GetAxis("L_XAxis_" + joystickString) * accel;
        acceleration.z += Input.GetAxis("L_YAxis_" + joystickString) * accel;

        friction = velocity.normalized * -1 * currFrict;

        acceleration += friction;

        velocity += acceleration;

        if (Input.GetButtonDown("X_" + joystickString) && boostCool <= 0)
        {
            // input.getaxisraw -> get direction of wanted boost: if between directions (-1, 0, and 1) boost in that direction
            velocity.x *= boost;
            velocity.z *= boost;
            boostCool = 3f;
            accel = boostRate;
        }
        else if (boostCool > 0)
            boostCool -= Time.deltaTime;
        else
            accel = accelRate;

        eggBod.velocity = velocity;
    }

    void OnCollisionEnter(Collision a)
    {
        if (bounced)
        {
            bounced = false;
            return;
        }
        if (a.gameObject.tag == "egg1" && id != 1)
            Bounce("egg1");
        else if (a.gameObject.tag == "egg2" && id != 2)
            Bounce("egg2");
        else if (a.gameObject.tag == "egg3" && id != 3)
            Bounce("egg3");
        else if (a.gameObject.tag == "egg4" && id != 4)
            Bounce("egg4");
		else if (a.gameObject.tag == "egg5" && id != 5)
			Bounce("egg5");
		else if (a.gameObject.tag == "egg6" && id != 6)
			Bounce("egg6");

        else if (a.gameObject.tag == "ground" && transform.position.y > -.1)
        {
            fallen = false;
            currFrict = mu;
        }
    }

    void OnCollisionExit(Collision b)
    {
        if (b.gameObject.tag == "ground")
        {
            fallen = true;
            currFrict = 0;
        }
    }

    // what if we made knockback a calculation of the eggs velocities, split them up proportionally, lower velocity egg bounces harder -> if close to exact velocity both will bounce hard(er)
    // fast hitting not moving -> fast doesn't bounce, slow bounces hard
    public void Bounce(string eggTag)
    {
        otherEgg = GameObject.FindGameObjectWithTag(eggTag).GetComponent<PlayerMovement>();
        otherEgg.bounced = true;

        Vector3 direction = (otherEgg.transform.position - transform.position).normalized;
        Vector3 oppDirection = direction * -1;
        Vector3 impact = Vector3.Dot(velocity, direction) * direction;
        Vector3 oppImpact = Vector3.Dot(otherEgg.velocity, oppDirection) * oppDirection;

        otherEgg.velocity += impact * knockPower;
        velocity -= impact;

        velocity += oppImpact * knockPower;
        otherEgg.velocity -= oppImpact;
    }
}
