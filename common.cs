using Godot;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace rpg
{

    public enum State
    {
        Idle = 0,
        Walk = 1,
        Combat = 2
    }

    public enum ObjectType
    {
        Player = 0,
        Enemy = 1,
        Npc = 2
    }

    public class Backpack
    {
        public Backpack(Player Player)
        {
            p = Player;
            items = new Dictionary<int, ItemSlot>();
            for (int i = 0; i < backpackSize; i++)
            {
                items.Add(i, new ItemSlot(i));
            }
        }
        public class ItemSlot
        {
            public ItemSlot(int id)
            {
                slotId = id;
                i = null;
            }

            public int AddItem(item item)
            {
                if (i is null)
                {
                    i = item;
                    amount++;
                    return 0;               // success
                }
                else if (i.Id == item.Id && i.Stackable)
                {
                    if (amount < i.MaxStackSize)
                    {
                        amount++;
                        return 0;
                    }
                }
                return 1;
            }

            public void RemoveItem()
            {
                i = null;
            }

            public int Getint()
            {
                return i is null ? -1 : i.Id;
            }
            public readonly int slotId;
            public item i;
            public int amount = 0;
        }
        public int AddItem(item item)
        {
            foreach (KeyValuePair<int, ItemSlot> pair in items)
            {
                ItemSlot slot = pair.Value;
                if (slot.AddItem(item) == 0)
                {
                    Debug.Print($"Added item ID: {slot.i.Id} in slot {slot.slotId}. Amount: {slot.amount}");
                    return 0;
                }
            }
            return 1;
        }

        //public int RemoveItem()

        public Dictionary<int, ItemSlot> items;
        private int backpackSize = 16;
        public Player p;
        public bool visible = false;
    }

    public class Statistics
    {
        public int str = 1;
        public int dex = 1;
        public int wis = 1;
        public int stam = 1;
    }

    public interface ICombat
    {
        public void StartCombat()
        {

        }

        public void StopCombat()
        {

        }

        public void Combat()
        {

        }
    }

    public class StateMachine
    {
        public State UpdateState(Vector2 vel, bool combat)
        {
            State state = new State();
            if (vel.IsZeroApprox())
            {
                state = State.Idle;
            }
            else
            {
                state = State.Walk;
            }
            if (combat)
            {
                state = State.Combat;
            }
            return state;
        }
    }

    public class AnimationController
    {
        public void OnStateChanged(State state, AnimatedSprite2D sprite) 
        {
            switch(state)
            {
                case State.Idle:
                    sprite.Play("idle");
                    Debug.Print("Playing IDLE anim!");
                    break;
                case State.Walk:
                    sprite.Play("walk");
                    Debug.Print("Playing WALK anim!");
                    break;
                default:
                    sprite.Play("idle");
                    Debug.Print("Playing DEFAULT anim!");
                    break;
            }
        }

        public void PlayIdle(AnimatedSprite2D sprite)
        {
            sprite.Play("idle");
        }
    }

}