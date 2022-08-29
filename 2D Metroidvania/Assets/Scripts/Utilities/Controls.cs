using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls {
  // DO NOT CHANGE OR RISK THE POSSIBLITY OF UNIVERSE IMPLOSION
  const string DEFAULT_KEYBOARD_JUMP = "Space";
  const string DEFAULT_KEYBOARD_ATTACK_1 = "Keypad4";
  const string DEFAULT_KEYBOARD_ATTACK_2 = "Keypad6";

  const string DEFAULT_GAMEPAD_JUMP = "JoystickButton2";
  const string DEFAULT_GAMEPAD_ATTACK_1 = "JoystickButton3";
  const string DEFAULT_GAMEPAD_ATTACK_2 = "JoystickButton1";

  // variables that define keys to use in game. To be changed only in pause menu
  public static string currentKeyboardJump = DEFAULT_KEYBOARD_JUMP;
  public static string currentKeyboardAttack1 = DEFAULT_KEYBOARD_ATTACK_1;
  public static string currentKeyboardAttack2 = DEFAULT_KEYBOARD_ATTACK_2;

  public static string currentGamepadJump = DEFAULT_GAMEPAD_JUMP;
  public static string currentGamepadAttack1 = DEFAULT_GAMEPAD_ATTACK_1;
  public static string currentGamepadAttack2 = DEFAULT_GAMEPAD_ATTACK_2;
}
