using UnityEngine;
using NUnit.Framework;
using UnityEngine.InputSystem;

public class InputServiceTests : InputTestFixture
{
    private InputService inputService;
    private Keyboard keyboard;
    private Gamepad gamepad;
    private Mouse mouse;
    
    [SetUp]
    public override void Setup()
    {
        base.Setup();

        keyboard = InputSystem.AddDevice<Keyboard>();
        gamepad = InputSystem.AddDevice<Gamepad>();
        mouse = InputSystem.AddDevice<Mouse>();

        inputService = new InputService();
        inputService.Enable();
    }
    
    [Test]
    public void InputService_CanBeCreated()
    {
        Assert.That(inputService, Is.Not.Null);
    }

    [Test]
    public void InputService_Enable_EnablesInput()
    {
        inputService.Enable();
        
        //Assert.That(inputService.IsEnabled, Is.True);
    }

    [Test]
    public void InputService_Disable_DisablesInput()
    {
        inputService.Disable();
        
        //Assert.That(inputService.IsEnabled, Is.False);
    }

    [Test]
    public void Move_WhenWPressed_ReturnsUp()
    {
        Press(keyboard.wKey);
        
        Assert.That(inputService.Move, Is.EqualTo(Vector2.up));
        
        Release(keyboard.wKey);
    }
    
    [Test]
    public void Move_WhenSPressed_ReturnsDown()
    {
        Press(keyboard.sKey);

        Assert.That(inputService.Move, Is.EqualTo(Vector2.down));

        Release(keyboard.sKey);
    }
    
    [Test]
    public void Move_WhenDPressed_ReturnsRight()
    {
        Press(keyboard.dKey);

        Assert.That(inputService.Move, Is.EqualTo(Vector2.right));

        Release(keyboard.dKey);
    }
    
    [Test]
    public void Move_WhenAPressed_ReturnsLeft()
    {
        Press(keyboard.aKey);

        Assert.That(inputService.Move, Is.EqualTo(Vector2.left));

        Release(keyboard.aKey);
    }
    
    [Test]
    public void Move_WhenWAndDPressed_ReturnsDiagonal()
    {
        Press(keyboard.wKey);
        Press(keyboard.dKey);

        var move = inputService.Move;

        Assert.That(move.magnitude, Is.EqualTo(1f).Within(0.001f));

        Release(keyboard.wKey);
        Release(keyboard.dKey);
    }
    
    [Test]
    public void Move_WhenGamepadLeftStickMoved_ReturnsStickValue()
    {
        Set(gamepad.leftStick, new Vector2(0.5f, 0.75f));

        var move = inputService.Move.normalized;
        var expected = new Vector2(0.5f, 0.75f).normalized;

        Assert.That(move.x, Is.EqualTo(expected.x).Within(0.01f));
        Assert.That(move.y, Is.EqualTo(expected.y).Within(0.01f));
    }
    
    [Test]
    public void Look_WhenMouseMoves_ReturnsMouseDelta()
    {
        Set(mouse.delta, new Vector2(15f, -7f));

        var look = inputService.Look;

        Assert.That(look.x, Is.EqualTo(15f).Within(0.001f));
        Assert.That(look.y, Is.EqualTo(-7f).Within(0.001f));
    }
    
    [Test]
    public void Look_WhenGamepadRightStickMoved_ReturnsStickValue()
    {
        Set(gamepad.rightStick, new Vector2(0.6f, -0.4f));

        var look = inputService.Look.normalized;
        var expected = new Vector2(0.6f, -0.4f).normalized;

        Assert.That(look.x, Is.EqualTo(expected.x).Within(0.01f));
        Assert.That(look.y, Is.EqualTo(expected.y).Within(0.01f));
    }
    
    [Test]
    public void Jump_WhenSpacePressed_ReturnsTrue()
    {
        Press(keyboard.spaceKey);
        

        Assert.That(inputService.JumpPressed, Is.True);

        Release(keyboard.spaceKey);
    }
    
    [Test]
    public void Attack_WhenMouseLeftButtonPressed_ReturnsTrue()
    {
        Press(mouse.leftButton);

        Assert.That(inputService.AttackPressed, Is.True);

        Release(mouse.leftButton);
    }
    
    [Test]
    public void Attack_WhenGamepadRightShoulderPressed_ReturnsTrue()
    {
        Press(gamepad.rightShoulder);

        Assert.That(inputService.AttackPressed, Is.True);

        Release(gamepad.rightShoulder);
    }
    
    [Test]
    public void Interact_WhenEPressed_ReturnsTrue()
    {
        Press(keyboard.eKey);

        Assert.That(inputService.InteractPressed, Is.True);

        Release(keyboard.eKey);
    }
    
    [Test]
    public void Interact_WhenGamepadWestButtonPressed_ReturnsTrue()
    {
        Press(gamepad.buttonWest);

        Assert.That(inputService.InteractPressed, Is.True);

        Release(gamepad.buttonWest);
    }
    
    [Test]
    public void Jump_WhenNothingPressed_ReturnsFalse()
    {

        Assert.That(inputService.JumpPressed, Is.False);
    }
    
    [Test]
    public void Jump_AfterRelease_ReturnsFalse()
    {
        Press(keyboard.spaceKey);

        Assert.That(inputService.JumpPressed, Is.True);

        Release(keyboard.spaceKey);

        InputSystem.Update();

        Assert.That(inputService.JumpPressed, Is.False);
    }
    
    [TearDown]
    public override void TearDown()
    {
        inputService.Disable();
        base.TearDown();
    }
}
