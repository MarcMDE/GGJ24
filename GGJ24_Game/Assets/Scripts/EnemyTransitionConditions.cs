using System;

[Serializable]
public class EnemyTransitionConditions
{
    public static readonly int Length = 6;

    public TriState ViewPlayer;
    public TriState EyeContact;
    public TriState Melee;
    public TriState EnoughTimeHidden;
    public TriState NoiseHeard;
    public TriState StateFinished;

    public TriState this[int index]
    {
        get
        {
            switch (index)
            {
                case 0:
                    return ViewPlayer;
                case 1: 
                    return EyeContact;
                case 2:
                    return Melee;
                case 3:
                    return EnoughTimeHidden;
                case 4: 
                    return NoiseHeard;
                case 5:
                    return StateFinished;

                default: return TriState.X;
            }
        }
        set
        {
            switch (index)
            {
                case 0:
                    ViewPlayer = value;
                    break;
                case 1:
                    EyeContact = value;
                    break;
                case 2:
                    Melee = value;
                    break;
                case 3:
                    EnoughTimeHidden = value;
                    break;
                case 4:
                    NoiseHeard = value;
                    break;
                case 5:
                    StateFinished = value;
                    break;

                default: break;
            }
        }
    }
}
