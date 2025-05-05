using JCC.Utils.DebugManager;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BlockMovement_UnitTests
{
    [SetUp]
    public void SetUp() 
    {
        DebugManager.Initialization(new DebugUnityImpl(), EDebugScope.All);
    }

    [TearDown]
    public void TearDown() 
    {
        DebugManager.Debug__ResetDebugManager();
    }

    [Test]
    public void Initialization_NullMovement_ErrorAndNotInitilized() 
    {
        //Arrange
        BlockMovement blockMovement = new GameObject("BlockMovement").AddComponent<BlockMovement>();
        IMovement movement = null;

        //Act
        bool result = blockMovement.Initialization(movement);

        //Assert
        LogAssert.Expect(LogType.Error, "BlockMovement.SetMovement :: movement is null");
        Assert.IsFalse(result);
        Assert.IsFalse(blockMovement.WasInitialized());
    }

    [Test]
    public void Initialization_CorrectMovement_IsInitilized()
    {
        //Arrange
        BlockMovement blockMovement = new GameObject("BlockMovement").AddComponent<BlockMovement>();
        IMovement movement = new MovementImpl_Fake();

        //Act
        bool result = blockMovement.Initialization(movement);

        //Assert
        Assert.IsTrue(result);
        Assert.IsTrue(blockMovement.WasInitialized());
    }
}
