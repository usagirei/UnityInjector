// --------------------------------------------------
// UnityInjector - Message.cs
// --------------------------------------------------

namespace UnityInjector
{
    public enum Message
    {
        /// Awake = Awake is called when the script instance is being loaded.
        Awake,

        /// FixedUpdate = This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
        FixedUpdate,

        /// LateUpdate = LateUpdate is called every frame, if the Behaviour is enabled.
        LateUpdate,

        /// OnAnimatorIK = Callback for setting up animation IK (inverse kinematics).
        OnAnimatorIK,

        /// OnAnimatorMove = Callback for processing animation movements for modifying root motion.
        OnAnimatorMove,

        /// OnApplicationFocus = Sent to all game objects when the player gets or loses focus.
        OnApplicationFocus,

        /// OnApplicationPause = Sent to all game objects when the player pauses.
        OnApplicationPause,

        /// OnApplicationQuit = Sent to all game objects before the application is quit.
        OnApplicationQuit,

        /// OnAudioFilterRead = If OnAudioFilterRead is implemented, Unity will insert a custom filter into the audio DSP chain.
        OnAudioFilterRead,

        /// OnBecameInvisible = OnBecameInvisible is called when the renderer is no longer visible by any camera.
        OnBecameInvisible,

        /// OnBecameVisible = OnBecameVisible is called when the renderer became visible by any camera.
        OnBecameVisible,

        /// OnCollisionEnter = OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider.
        OnCollisionEnter,

        /// OnCollisionEnter2D = Sent when an incoming collider makes contact with this object's collider (2D physics only).
        OnCollisionEnter2D,

        /// OnCollisionExit = OnCollisionExit is called when this collider/rigidbody has stopped touching another rigidbody/collider.
        OnCollisionExit,

        /// OnCollisionExit2D = Sent when a collider on another object stops touching this object's collider (2D physics only).
        OnCollisionExit2D,

        /// OnCollisionStay = OnCollisionStay is called once per frame for every collider/rigidbody that is touching rigidbody/collider.
        OnCollisionStay,

        /// OnCollisionStay2D = Sent each frame where a collider on another object is touching this object's collider (2D physics only).
        OnCollisionStay2D,

        /// OnConnectedToServer = Called on the client when you have successfully connected to a server.
        OnConnectedToServer,

        /// OnControllerColliderHit = OnControllerColliderHit is called when the controller hits a collider while performing a Move.
        OnControllerColliderHit,

        /// OnDestroy = This function is called when the MonoBehaviour will be destroyed.
        OnDestroy,

        /// OnDisable = This function is called when the behaviour becomes disabled () or inactive.
        OnDisable,

        /// OnDisconnectedFromServer = Called on the client when the connection was lost or you disconnected from the server.
        OnDisconnectedFromServer,

        /// OnDrawGizmos = Implement OnDrawGizmos if you want to draw gizmos that are also pickable and always drawn.
        OnDrawGizmos,

        /// OnDrawGizmosSelected = Implement this OnDrawGizmosSelected if you want to draw gizmos only if the object is selected.
        OnDrawGizmosSelected,

        /// OnEnable = This function is called when the object becomes enabled and active.
        OnEnable,

        /// OnFailedToConnect = Called on the client when a connection attempt fails for some reason.
        OnFailedToConnect,

        /// OnFailedToConnectToMasterServer = Called on clients or servers when there is a problem connecting to the MasterServer.
        OnFailedToConnectToMasterServer,

        /// OnGUI = OnGUI is called for rendering and handling GUI events.
        OnGUI,

        /// OnJointBreak = Called when a joint attached to the same game object broke.
        OnJointBreak,

        /// OnLevelWasLoaded = This function is called after a new level was loaded.
        OnLevelWasLoaded,

        /// OnMasterServerEvent = Called on clients or servers when reporting events from the MasterServer.
        OnMasterServerEvent,

        /// OnMouseDown = OnMouseDown is called when the user has pressed the mouse button while over the GUIElement or Collider.
        OnMouseDown,

        /// OnMouseDrag = OnMouseDrag is called when the user has clicked on a GUIElement or Collider and is still holding down the mouse.
        OnMouseDrag,

        /// OnMouseEnter = Called when the mouse enters the GUIElement or Collider.
        OnMouseEnter,

        /// OnMouseExit = Called when the mouse is not any longer over the GUIElement or Collider.
        OnMouseExit,

        /// OnMouseOver = Called every frame while the mouse is over the GUIElement or Collider.
        OnMouseOver,

        /// OnMouseUp = OnMouseUp is called when the user has released the mouse button.
        OnMouseUp,

        /// OnMouseUpAsButton = OnMouseUpAsButton is only called when the mouse is released over the same GUIElement or Collider as it was pressed.
        OnMouseUpAsButton,

        /// OnNetworkInstantiate = Called on objects which have been network instantiated with Network.Instantiate.
        OnNetworkInstantiate,

        /// OnParticleCollision = OnParticleCollision is called when a particle hits a collider.
        OnParticleCollision,

        /// OnPlayerConnected = Called on the server whenever a new player has successfully connected.
        OnPlayerConnected,

        /// OnPlayerDisconnected = Called on the server whenever a player disconnected from the server.
        OnPlayerDisconnected,

        /// OnPostRender = OnPostRender is called after a camera finished rendering the scene.
        OnPostRender,

        /// OnPreCull = OnPreCull is called before a camera culls the scene.
        OnPreCull,

        /// OnPreRender = OnPreRender is called before a camera starts rendering the scene.
        OnPreRender,

        /// OnRenderImage = OnRenderImage is called after all rendering is complete to render image.
        OnRenderImage,

        /// OnRenderObject = OnRenderObject is called after camera has rendered the scene.
        OnRenderObject,

        /// OnSerializeNetworkView = Used to customize synchronization of variables in a script watched by a network view.
        OnSerializeNetworkView,

        /// OnServerInitialized = Called on the server whenever a Network.InitializeServer was invoked and has completed.
        OnServerInitialized,

        /// OnTransformChildrenChanged = This function is called when the list of children of the transform of the GameObject has changed.
        OnTransformChildrenChanged,

        /// OnTransformParentChanged = This function is called when the parent property of the transform of the GameObject has changed.
        OnTransformParentChanged,

        /// OnTriggerEnter = OnTriggerEnter is called when the Collider other enters the trigger.
        OnTriggerEnter,

        /// OnTriggerEnter2D = Sent when another object enters a trigger collider attached to this object (2D physics only).
        OnTriggerEnter2D,

        /// OnTriggerExit = OnTriggerExit is called when the Collider other has stopped touching the trigger.
        OnTriggerExit,

        /// OnTriggerExit2D = Sent when another object leaves a trigger collider attached to this object (2D physics only).
        OnTriggerExit2D,

        /// OnTriggerStay = OnTriggerStay is called once per frame for every Collider other that is touching the trigger.
        OnTriggerStay,

        /// OnTriggerStay2D = Sent each frame where another object is within a trigger collider attached to this object (2D physics only).
        OnTriggerStay2D,

        /// OnValidate = This function is called when the script is loaded or a value is changed in the inspector (Called in the editor only).
        OnValidate,

        /// OnWillRenderObject = OnWillRenderObject is called once for each camera if the object is visible.
        OnWillRenderObject,

        /// Reset = Reset to default values.
        Reset,

        /// Start = Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
        Start,

        /// Update = Update is called every frame, if the MonoBehaviour is enabled.
        Update,
    }
}
