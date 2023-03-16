#  Publish Subscribe Framework

A small Unity Framework for Sending & Receiving Message through Signal without concrete implementation needed

[Simpler PubSub](https://github.com/StinkySteak/com.stinkysteak.simplepubsub)


## Usage/Examples

### Signal Structure
```csharp
public struct PlayerDamagedSignal : ISignal 
{
    public int ReduceAmount;
}
```

### Listener Implementation
```csharp
public class HealthHandler : MonoBehaviour 
{
    private void OnSignalReceived(PlayerDamagedSignal signal)
        => Debug.Log($"Reduce Amount: {signal.ReduceAmount}");
}
```

### Publisher Implementation
```csharp
public class PlayerHealth : MonoBehaviour 
{
    private void OnDamageReceived(int amount)
    {
        SignalManager.Publish(new PlayerDamagedSignal() 
        {
            ReduceAmount = amount
        });
    }
}
```

# Core Structure

`SignalManager`: Handles for Publishing & Subscribing Signals

`Subscription` : Relation between Listener and Signal Type

`ISignal` : Interface for signal messaging object
