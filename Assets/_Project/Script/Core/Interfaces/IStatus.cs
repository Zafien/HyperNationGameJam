using UniRx;

public interface IStatus
{
    void AddStatus(Status status);
    void RemoveStatus(Status status);

}

[System.Serializable]
public class Status
{
    public Subject<Unit> OnStatusEffect = new Subject<Unit>();

}

