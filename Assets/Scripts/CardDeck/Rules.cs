using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
//using ARTCards;
namespace ARTCards.Rules
{
	[XmlRoot("Rules")]
	public class Rules {

		[XmlElement("Deck", typeof(DeckData))]
		public readonly DeckData deck;
		//public readonly int deckSize;
		public readonly int handSize;

		[XmlArray("Attributes")]
		[XmlArrayItem("Attribute")]
		public readonly List<AttributeData> attributes;

		[XmlArray("Stats")]
		[XmlArrayItem("Stat")]
		public readonly List<StatData> stats;

		[XmlElement("Units")]
		public readonly UnitData unit;

		[XmlElement("Player", typeof(PlayerData))]
		public readonly PlayerData player;

		public Rules (){
			//attributes = new List<AttributeData>();
		}

		public static Rules Load(string _xml){
			
			//Load arrays
			XmlSerializer serializer = new XmlSerializer(typeof (Rules));
			StringReader reader = new StringReader(_xml);

			Rules rules = serializer.Deserialize(reader) as Rules;
			//this = serializer.Deserialize(reader) as Rules;
			reader.Close();

			//Load single variables

			return rules;
		}

	}
	//[XmlRoot("Deck")]
	public class DeckData{
		[XmlElement("Size")]
		public readonly int deckSize;

		[XmlArray("Cards")]
		[XmlArrayItem("Card")]
		public readonly List<CardData> cards;
	}


	public class CardData{
		[XmlElement("attr")]
		public readonly int[] attrs;
		[XmlAttribute("id")]
		public string id;
	}

	public class AttributeData{
		[XmlAttribute("name")]
		public readonly string name;
		[XmlAttribute("min")]
		public readonly int minValue;
		[XmlAttribute("max")]
		public readonly int maxValue;

		public Attribute ToAttribute(){
			return new Attribute(name, 7, minValue, maxValue);
		}
	}

	public class StatData{
		[XmlAttribute("name")]
		public readonly string name;
		[XmlAttribute("attr")]
		public readonly string attr;
		[XmlArray("Calc")]
		[XmlArrayItem("value")]
		public readonly string[] calc;

		public SecondaryAttribute ToSecAttribute(){
			return new SecondaryAttribute(name, attr, calc);
		}
	}
	public class PlayerData{
		[XmlElement("Hand")]
		public readonly int handSize;
		[XmlElement("Units")]
		public readonly int unitNumber;
		[XmlElement("CommandPoints")]
		public readonly int commandPoints;
	}
	public class UnitData{
		//[XmlElement("DamageFormula")]
		[XmlAttribute("formula")]
		public readonly string dmgFormula;
	}
}