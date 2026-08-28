using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;

public class CameraControllerTests
{
  private FakeCameraMode firstPerson;
  private FakeCameraMode thirdPerson;
  private List<ICameraMode> modes;
  private CameraController controller;
  private FakeCameraModeInput cameraInput;

  [SetUp]
  public void Setup()
  {
    firstPerson = new FakeCameraMode(CameraModeType.FirstPerson);
    thirdPerson = new FakeCameraMode(CameraModeType.ThirdPerson);
    cameraInput = new FakeCameraModeInput();
    modes = new List<ICameraMode>
    {
      firstPerson,
      thirdPerson
    };
    
   controller = new CameraController(modes, CameraModeType.FirstPerson, cameraInput );
  }
  
  [Test]
  public void Constructor_CreatesController()
  {
    Assert.That(controller, Is.Not.Null);
  }
  
  [Test]
  public void Constructor_SetsFirstPersonAsCurrentMode()
  {
    Assert.That(controller.CurrentMode, Is.SameAs(firstPerson));
  }
  
  [Test]
  public void Constructor_ActivatesFirstPersonMode()
  {
    Assert.That(firstPerson.IsActivated, Is.True);
    Assert.That(thirdPerson.IsActivated, Is.False);
  }

  [Test]
  public void SetMode_ThirdPerson_SwitchesCurrentMode()
  {
    controller.SetMode(CameraModeType.ThirdPerson);

    Assert.That(controller.CurrentMode, Is.SameAs(thirdPerson));
  }
  
  [Test]
  public void SetMode_ThirdPerson_DeactivatesFirstPerson()
  {
    controller.SetMode(CameraModeType.ThirdPerson);

    Assert.That(firstPerson.IsDeactivated, Is.True);
  }
  
  [Test]
  public void SetMode_ThirdPerson_ActivatesThirdPerson()
  {
    controller.SetMode(CameraModeType.ThirdPerson);

    Assert.That(thirdPerson.IsActivated, Is.True);
  }
  
  [Test]
  public void SetMode_ThirdPerson_DeactivatesFirstPersonOnce()
  {
    controller.SetMode(CameraModeType.ThirdPerson);

    Assert.That(firstPerson.DeactivateCount, Is.EqualTo(1));
  }

  [Test]
  public void SetMode_ThirdPerson_ActivatesThirdPersonOnce()
  {
    controller.SetMode(CameraModeType.ThirdPerson);

    Assert.That(thirdPerson.ActivateCount, Is.EqualTo(1));
  }
  
  [Test]
  public void SetMode_FirstPerson_WhenAlreadyActive_DoesNotReactivate()
  {
    controller.SetMode(CameraModeType.FirstPerson);

    Assert.That(firstPerson.ActivateCount, Is.EqualTo(1));
    Assert.That(firstPerson.DeactivateCount, Is.EqualTo(0));
  }
  
  [Test]
  public void SetMode_FirstPerson_ThirdPerson_FirstPerson_SwitchesCorrectly()
  {
    controller.SetMode(CameraModeType.ThirdPerson);
    controller.SetMode(CameraModeType.FirstPerson);

    Assert.That(controller.CurrentMode, Is.SameAs(firstPerson));

    Assert.That(firstPerson.ActivateCount, Is.EqualTo(2));
    Assert.That(firstPerson.DeactivateCount, Is.EqualTo(1));

    Assert.That(thirdPerson.ActivateCount, Is.EqualTo(1));
    Assert.That(thirdPerson.DeactivateCount, Is.EqualTo(1));
  }
  
  [Test]
  public void SetMode_Generic_ThirdPerson_SwitchesToThirdPerson()
  {
    var firstPerson = new FakeFirstPersonCamera();
    var thirdPerson = new FakeThirdPersonCamera();
  
    var controller = new CameraController(
      new List<ICameraMode>
      {
        firstPerson,
        thirdPerson
      }, CameraModeType.FirstPerson, cameraInput);
  
    controller.SetMode<FakeThirdPersonCamera>();
  
    Assert.That(controller.CurrentMode, Is.SameAs(thirdPerson));
  }
  
  [Test]
  public void Tick_WhenSwitchCameraPressed_SwitchesToThirdPerson()
  {
    cameraInput.SwitchCameraPressed = true;

    controller.Tick();

    Assert.That(
      controller.CurrentMode,
      Is.SameAs(thirdPerson));
  }
  
  [Test]
  public void Tick_WhenSwitchCameraPressed_SwitchesBackToFirstPerson()
  {
    controller.SetMode(CameraModeType.ThirdPerson);

    cameraInput.SwitchCameraPressed = true;

    controller.Tick();

    Assert.That(
      controller.CurrentMode,
      Is.SameAs(firstPerson));
  }
  
  [Test]
  public void Tick_WhenSwitchCameraNotPressed_DoesNothing()
  {
    controller.Tick();

    Assert.That(
      controller.CurrentMode,
      Is.SameAs(firstPerson));
  }
  
  [Test]
  public void Tick_WhenSwitchCameraPressed_DeactivatesCurrentMode()
  {
    cameraInput.SwitchCameraPressed = true;

    controller.Tick();

    Assert.That(firstPerson.IsDeactivated, Is.True);
  }
  
  [Test]
  public void Tick_WhenSwitchCameraPressed_ActivatesNewMode()
  {
    cameraInput.SwitchCameraPressed = true;

    controller.Tick();

    Assert.That(thirdPerson.IsActivated, Is.True);
  }
  
  [Test]
  public void Tick_WhenSwitchCameraPressedOnce_SwitchesOnlyOnce()
  {
    cameraInput.SwitchCameraPressed = true;

    controller.Tick();

    Assert.That(
      controller.CurrentMode,
      Is.SameAs(thirdPerson));

    cameraInput.SwitchCameraPressed = false;

    controller.Tick();

    Assert.That(
      controller.CurrentMode,
      Is.SameAs(thirdPerson));
  }

  [TearDown]
  public void TearDown(){}
}


