using UnityEngine;
using System.Collections;
using VRTK;
using System.Collections.Generic;

public class Wand : VRTK_InteractableObject {

    private CircleSelector circleSelector;
    private Dictionary<SelectionSequence, BaseSpell> spellSequences;

    public override void Grabbed(GameObject currentGrabbingObject)
    {
        base.Grabbed(currentGrabbingObject);
        circleSelector = gameObject.AddComponent<CircleSelector>();
        spellSequences = SpellContainer.GetSpellDictionary();
    }

    /* When the button designated by the use alias is pressed, create a circle to select a spell */
    public override void StartUsing(GameObject currentUsingObject)
    {
        base.StartUsing(currentUsingObject);

        Destroy(GetComponent<BaseSpell>());
        GetComponent<CircleSelector>().CreateCircle(Resources.Load("Weapons/orb") as GameObject, 4, .2f, .2f,1);
        
    }

    public override void StopUsing(GameObject previousUsingObject)
    {
        base.StopUsing(previousUsingObject);
        //if(CheckSequence())
        //{


        //}
        CheckSequence();
        circleSelector.CenterNodes();
        circleSelector.DestroyCircle(.2f);

    }

    public bool CheckSequence()
    {
        foreach (KeyValuePair<SelectionSequence, BaseSpell> entry in spellSequences)
        {
            if (circleSelector.CheckSelectionSequence(entry.Key))
            {
                BaseSpell spell = entry.Value;
                gameObject.AddComponent(spell.GetType());
                return true;
            }
        }
        return false;
    }


}
