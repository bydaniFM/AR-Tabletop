using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Security.AccessControl;
using ARTCards;
using ARTCards.Rules;

public class RulesLoader : MonoBehaviour {
	public string path;
	string settingsPath;
	private Rules rules;

	static RulesLoader instance;
	// Use this for initialization
	void Awake(){
		if (instance == null){
			instance = this;
		}
		else {
			Destroy(gameObject);
			Debug.LogError("An instance of RulesLoader already exists");
		}
		settingsPath = Application.dataPath + "/Settings/";
		#if UNITY_STANDALONE_WIN
			path = path.Replace('/', '\\');
		#endif    

		FirstRun();

		rules = Rules.Load(File.ReadAllText(settingsPath+"_rules.xml"));
	}
	void Start () {
		
		Debug.Log(rules.attributes[0].name);
		Debug.Log(rules.stats[0].name);
		Debug.Log(rules.stats[0].calc[1]);
		Debug.Log("Cards in deck "+rules.deck.deckSize);
		Debug.Log(rules.deck.cards[0].attrs[1]);
		Debug.Log(rules.deck.cards[1].attrs[1]);

		//Debug.Log(rules.player.handSize);
		//Debug.Log(rules.player.unitNumber);
		//Debug.Log(rules.player.commandPoints);
		Debug.Log(rules.unit.dmgFormula);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static int deckSize{
		get {
			return instance.rules.deck.deckSize;
		}
	}
	public static string damageFormula{
		get {
			return instance.rules.unit.dmgFormula;
		}
	}


	public static int unitNumber{
		get {
			return instance.rules.player.unitNumber;
		}
	}
	public static int handSize{
		get {
			return instance.rules.player.handSize;
		}
	}

	public static List<PlayingCard> GetPregenCards(){
		List<PlayingCard> result = instance.rules.deck.cards.ConvertAll( x => new PlayingCard(x.attrs)); 
		return result;
	}

	public static int GetMinAttribute(string attrName){
		AttributeData attr = instance.rules.attributes.Single(x => x.name == attrName);
		return attr.minValue;
	}

	public static Dictionary<string, Attribute> GetAttributesDict(){
		return instance.rules.attributes.ToDictionary( x => x.name, x=> x.ToAttribute());
	}

	public static Dictionary<string, SecondaryAttribute> GetStatsDict(){
		return instance.rules.stats.ToDictionary( x => x.name, x => x.ToSecAttribute());
	}

	void FirstRun(){
		
		string xml = Resources.Load<TextAsset>(path).text;

		if (!Directory.Exists(settingsPath)){
			Directory.CreateDirectory(settingsPath);

			File.WriteAllText(settingsPath+"_rules.xml", xml);
			//File.SetAttributes(settingsPath+"_rules.xml", FileAttributes.Normal);
		}
		else if (!File.Exists(settingsPath+"_rules.xml")){
			File.WriteAllText(settingsPath+"_rules.xml", xml);
		}
	}

}
