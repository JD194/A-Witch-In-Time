using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpellUIScript : MonoBehaviour
{
    public int selectedRuneSlot;
    public int[] runeSlots;
    public GameObject[] runeSlotVisuals;
    public Sprite[] runeImages;
    public TextMeshProUGUI spellLeft;
    public TextMeshProUGUI spellRight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Sets value for currently selected rune slot
    public void SelectSlot(int selectedSlot)
    {
        selectedRuneSlot = selectedSlot;
    }
    //Sets current rune slot selected to teh rune selected from the UI
    public void SelectRune(int runeSelected)
    {
        if(0 <= selectedRuneSlot && selectedRuneSlot <= 3)
        {
            runeSlots[selectedRuneSlot] = runeSelected;
            UpdateSpell();
        }
    }

    //Updates the relevant hands spell based on runes selected.
    private void UpdateSpell()
    {
        if(selectedRuneSlot == 0 || selectedRuneSlot == 1)
        {
            spellLeft.SetText(GetSpell(runeSlots[0], runeSlots[1]).ToString());
        }
        else if(selectedRuneSlot == 2 || selectedRuneSlot == 3)
        {
            spellRight.SetText((runeSlots[2] + runeSlots[3]).ToString());
        }
    }

    //Checks selsected runes for relevant hand and returns the spell code these combine to create
    private int GetSpell(int runeA, int runeB)
    {
        int spellCreated = 0;
        //Rune A = Fire
        if(runeA == 0)
        {
            if(runeB == 0)
            {
                spellCreated = 0;
            }
            else if(runeB == 1)
            {
                spellCreated = 1;
            }
            else if(runeB == 2)
            {
                spellCreated = 2;
            }
            else if (runeB == 3)
            {
                spellCreated = 3;
            }
            else if (runeB == 4)
            {
                spellCreated = 4;
            }
        }
        //Rune A = Water
        else if (runeA == 1)
        {
            if (runeB == 0)
            {
                spellCreated = 1;
            }
            else if (runeB == 1)
            {
                spellCreated = 5;
            }
            else if (runeB == 2)
            {
                spellCreated = 6;
            }
            else if (runeB == 3)
            {
                spellCreated = 7;
            }
            else if (runeB == 4)
            {
                spellCreated = 8;
            }
        }
        //Rune A = Air
        else if (runeA == 2)
        {
            if (runeB == 0)
            {
                spellCreated = 2;
            }
            else if (runeB == 1)
            {
                spellCreated = 6;
            }
            else if (runeB == 2)
            {
                spellCreated = 9;
            }
            else if (runeB == 3)
            {
                spellCreated = 10;
            }
            else if (runeB == 4)
            {
                spellCreated = 11;
            }
        }
        //Rune A = Earth
        else if (runeA == 3)
        {
            if (runeB == 0)
            {
                spellCreated = 3;
            }
            else if (runeB == 1)
            {
                spellCreated = 7;
            }
            else if (runeB == 2)
            {
                spellCreated = 10;
            }
            else if (runeB == 3)
            {
                spellCreated = 12;
            }
            else if (runeB == 4)
            {
                spellCreated = 13;
            }
        }
        //Rune A = Force
        else if (runeA == 4)
        {
            if (runeB == 0)
            {
                spellCreated = 4;
            }
            else if (runeB == 1)
            {
                spellCreated = 8;
            }
            else if (runeB == 2)
            {
                spellCreated = 11;
            }
            else if (runeB == 3)
            {
                spellCreated = 13;
            }
            else if (runeB == 4)
            {
                spellCreated = 14;
            }
        }

        return spellCreated;
    }

    /* Spell List
     * 
     *  Fire(0) + Fire(0) = Torch (0)
     *  Fire(0) + Water(1) = Steam (1)
     *  Fire(0) + Air(2) = Lightning (2)
     *  Fire(0) + Earth(3) = Magma Splash (3)
     *  Fire(0) + Force(4)  Fire Shot (4)
     *  
     *  Water(1) + Fire(0) = Steam (1)
     *  Water(1) + Water(1) = Create Water (5)
     *  Water(1) + Air(2) = Cloud (6)
     *  Water(1) + Earth(3) = Mud Spray (7)
     *  Water(1) + Force(4) = Water Stream (8)
     *  
     *  Air(2) + Fire(0) = Lightning (2)
     *  Air(2) + Water(1) = Cloud (6)
     *  Air(2) + Air(2) = Breeze (9)
     *  Air(2) + Earth(3) Dust Cloud (10)
     *  Air(2) + Force(4) Air Blade (11)
     *  
     *  Earth(3) + Fire(0) = Magma Splash (3)
     *  Earth(3) + Water(1) = Mud Spray (7)
     *  Earth(3) + Air(2) = Dust Cloud (10)
     *  Earth(3) + Earth(3) = Earth Shield (12)
     *  Earth(3) + Force(4) = Rock Bullet (13)
     *  
     *  Force(4) + Fire(0) = Fire Shot (4)
     *  Force(4) + Water(1) = Water Stream (8)
     *  Force(4) + Air(2) = Air Blade (11)
     *  Force(4) + Earth(3) = Rock Bullet (13)
     *  Force(4) + Force(4) = Crush (14)
     *  
     */
}
