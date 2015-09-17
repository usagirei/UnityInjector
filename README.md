### Installing

* Place the Contents of the '**Managed**' Directory into the Game '**[*ExeName*]_Data\Managed**' Directory [1]
* Place the Contents of the '**ReiPatcher**' Directory into the ReiPatcher '**Patches**' Directory [2]
  *Not the Root Directory! The Patches Directory!*
* Place the Contents of the '**UnityInjector**' Directory into the '**UnityInjector**' Directory at the **root** of the Game Directory, alongside the *Game Exe* and *Data* Directory [3]

[1] Core/Hook Dlls go in this directory
[2] Patcher Dlls go in this directory
[3] Plugin Dlls go in this directory

---
### DebugPlugin

Included by default in the Core UnityInjector is the DebugPlugin, which provides a **Windows Console** with the game output log, alongside other plugin messages

|Configuration |Type             |Description                 |
|--------------|-----------------|----------------------------|
|Enabled       |Boolean          |Enables DebugPlugin         |
|Mirror        |Boolean          |Mirrors output to Debug.log |
|CodePage      |Unsigned Integer |Forces a Specific CodePage  |

