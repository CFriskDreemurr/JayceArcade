using UnityEngine;


public enum StickTypes
{
    Normal,
    Double,
    Triple
}
public class RingoStick : MonoBehaviour
{
    [SerializeField] private StickTypes stickType;

    private int basePoints = 10;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ringo"))
        {
            switch (stickType)
            {
                case StickTypes.Normal:
                    RingoGame.Instance.GetPoints(basePoints);
                    break;

                case StickTypes.Double:
                    RingoGame.Instance.GetPoints(basePoints * 2);
                    break;

                case StickTypes.Triple:
                    RingoGame.Instance.GetPoints(basePoints * 3);
                        break;

            }
        }
    }
}
