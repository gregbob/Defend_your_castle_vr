using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using VRTK;

public class MagicCircle : CircleSelector {

    private Dictionary<SelectionSequence, BaseSpell> spellSequences;

    protected override void Awake()
    {
        base.Awake();
        spellSequences = SpellContainer.GetSpellDictionary();

    }


    protected override void Start()
    {
        base.Start();
        var events = GetComponentInParent<VRTK_ControllerEvents>();
        if (events == null)
        {
            Debug.Log("Controller events is null. Calling from MagicCircle");
            return;
        }

        // When touchpad is released, check to see if any sequences have been correctly selected
        events.TouchpadReleased += new ControllerInteractionEventHandler(CheckSequence);
        events.TouchpadReleased += new ControllerInteractionEventHandler(CollapseCircle);
        

    }
    /// <summary>
    /// Check to see if sequence has been correctly drawn. If so, add spell component to gameobject
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void CheckSequence(object sender, ControllerInteractionEventArgs e)
    {
        
        foreach(KeyValuePair<SelectionSequence, BaseSpell> entry in spellSequences)
        {
            if (CheckSelectionSequence(entry.Key)) {
                BaseSpell spell = entry.Value;
                gameObject.AddComponent(spell.GetType());
                CenterNodes();
            }
        }
    }

    public void CollapseCircle(object sender, ControllerInteractionEventArgs e)
    {
        DestroyCircle(.25f);
    }


}
