
public abstract class CharacterState
{
    private CharacterState currentCharacterState;

    private void Init()
    {
        
    }
    
    /// <summary>
    /// Returns a Boolean value indicating whether it is valid for the state machine to transition from its current state to a state of the specified class.
    /// </summary>
    /// <returns></returns>
    private bool CanEnterState()
    {
        return false;
    }
    
    /// <summary>
    /// Attempts to transition the state machine from its current state to a state of the specified class.
    /// </summary>
    /// <returns></returns>
    private bool Enter()
    {
        return false;
    }
    
    /// <summary>
    /// Tells the current state object to perform per-frame updates.
    /// </summary>
    private void Update()
    {
    }

}
