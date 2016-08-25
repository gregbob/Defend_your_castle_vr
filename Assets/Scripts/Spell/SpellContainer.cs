using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpellContainer  {

    private static SelectionSequence CCW = new SelectionSequence(new int[] { 0, 3, 2, 1 }, "CCW");
    private static SelectionSequence CW = new SelectionSequence(new int[] { 0, 1, 2, 3 }, "CW");
    private static SelectionSequence NS = new SelectionSequence(new int[] { 0, 2 }, "NS");

    public static Dictionary<SelectionSequence, BaseSpell> GetSpellDictionary()
    {
        Dictionary<SelectionSequence, BaseSpell> spellSequences;
        GameObject spellContainer = new GameObject("_SpellContainer");

        spellSequences = new Dictionary<SelectionSequence, BaseSpell>();

        spellSequences.Add(CCW, new BoomSpell());
        spellSequences.Add(CW, new ThunderSpell());
        spellSequences.Add(NS, new FireballSpell());

        //DisableAllSpells(spellContainer);

        return spellSequences;
    }

    private static void DisableAllSpells(GameObject spellContainer)
    {
        BaseSpell[] spells = spellContainer.GetComponents<BaseSpell>();
        for (int i = 0; i < spells.Length; i++)
        {
            spells[i].enabled = false;
        }
    }
}
