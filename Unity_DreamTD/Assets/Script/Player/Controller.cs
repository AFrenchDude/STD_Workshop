//By ALBERT Esteban
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR //Only use the library when using Editor, prevents bugs with a build by ignoring it

using UnityEditor; //Required for custom inspector

#endif


public class Controller : MonoBehaviour
{
    [SerializeField] Camera _camera = null;
    //[SerializeField] [Range(1, 10)] private int _integerRange;

    //Following attributes are serialized for the custom inspector to work and hidden in inspector so they don't appear twice (normally + custom inspector)
    [SerializeField] [HideInInspector] float _speed = 10.0f;
    [SerializeField] [HideInInspector] float _speedZoom = 4;
    [SerializeField] [HideInInspector] int _zoomNumber = 2;
    [SerializeField] [HideInInspector] private bool _hasMoveBoundaries = false;
    [SerializeField] [HideInInspector] private bool _showParameters = false;
    [SerializeField] [HideInInspector] private float _minX = 0.0f;
    [SerializeField] [HideInInspector] private float _maxX;
    [SerializeField] [HideInInspector] private float _minZ = 0.0f;
    [SerializeField] [HideInInspector] private float _maxZ;

    private int _zoomStep;
    private float _zoomCD = 0.05f;
    private float _lastZoomTime = 0.0f;


    #region Editor
#if UNITY_EDITOR //This function use UnityEditor namespace content, make sure it only runs when using the editor

    [CustomEditor(typeof(Controller))]
    [System.Serializable]
    public class ControllerRangeEditor : Editor //create a public class derived from Editor (see UnityEditor namespace)
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Controller controller = (Controller)target; //Allows to get the vars in the customInspector

            controller._showParameters = EditorGUILayout.Foldout(controller._showParameters, "Parameters", true);

            if (controller._showParameters)
            {
                EditorGUILayout.LabelField("Camera Speed");
                controller._speed = EditorGUILayout.FloatField(controller._speed, GUILayout.MaxWidth(50));
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Has movement boundaries");
                controller._hasMoveBoundaries = EditorGUILayout.Toggle(controller._hasMoveBoundaries);
                if (controller._hasMoveBoundaries)
                {
                    DrawCameraMovementBordersField(controller);
                }

                DrawZoomBordersField(controller);
            }
        }

        //For Explanations check DrawZoomBordersField instead of DrawCameraMovementBordersField
        private static void DrawCameraMovementBordersField(Controller controller)
        {
            EditorGUILayout.LabelField("Movement borders");
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Min X", GUILayout.MaxWidth(80));
            controller._minX = EditorGUILayout.FloatField(controller._minX, GUILayout.MaxWidth(50));

            EditorGUILayout.LabelField("Max X", GUILayout.MaxWidth(80));
            controller._maxX = EditorGUILayout.FloatField(controller._maxX, GUILayout.MaxWidth(50));

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Min Z", GUILayout.MaxWidth(80));
            controller._minZ = EditorGUILayout.FloatField(controller._minZ, GUILayout.MaxWidth(50));

            EditorGUILayout.LabelField("Max Z", GUILayout.MaxWidth(80));
            controller._maxZ = EditorGUILayout.FloatField(controller._maxZ, GUILayout.MaxWidth(50));

            EditorGUILayout.EndHorizontal();
        }

        private static void DrawZoomBordersField(Controller controller)
        {
            EditorGUILayout.Space(); //Add empty space
            EditorGUILayout.LabelField("Zoom parameters"); //Add category name
            EditorGUILayout.BeginHorizontal(); //Being a field to have multiple vars on same line

            EditorGUILayout.LabelField("Zoom Speed", GUILayout.MaxWidth(80)); //Give visual name for var to set, Width of the field is 100
            controller._speedZoom = EditorGUILayout.FloatField(controller._speedZoom, GUILayout.MaxWidth(50)); //Make space to set variable

            EditorGUILayout.LabelField("Zoom Number", GUILayout.MaxWidth(80));
            controller._zoomNumber = EditorGUILayout.IntField(controller._zoomNumber, GUILayout.MaxWidth(50));

            EditorGUILayout.EndHorizontal();
        }

    }

#endif
    #endregion


    private void Awake()
    {
        if (_zoomNumber < 0)
        {
            Debug.LogWarningFormat("Zoom Steps Number ({0}), is inferior to 0, defaulted value to 0", _zoomNumber);
            _zoomNumber = 0;
        }
    }

    private void Update()
    {

        Move();

        float mouseWheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (mouseWheelInput > 0 && _zoomStep < _zoomNumber && Time.time >= _lastZoomTime + _zoomCD)
        {
            transform.position = transform.position + _camera.transform.forward * _speedZoom;
            _zoomStep++;
            _lastZoomTime = Time.time;
        }
        if (mouseWheelInput < 0 && _zoomStep > 0 && Time.time >= _lastZoomTime + _zoomCD)
        {
            transform.position = transform.position + _camera.transform.forward * -_speedZoom;
            _zoomStep--;
            _lastZoomTime = Time.time;
        }
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        transform.position = transform.position + new Vector3(horizontalInput * _speed * Time.deltaTime, 0, verticalInput * _speed * Time.deltaTime);

        GetIsInMovementBorders(out bool isAtMinX, out bool isAtMaxX, out bool isAtMinZ, out bool isAtMaxZ);
        if (isAtMinX)
        {
            var correctedPosition = transform.position;
            correctedPosition.x = _minX;
            transform.position = correctedPosition;
        }
        if (isAtMaxX)
        {
            var correctedPosition = transform.position;
            correctedPosition.x = _maxX;
            transform.position = correctedPosition;
        }
        if (isAtMinZ)
        {
            var correctedPosition = transform.position;
            correctedPosition.z = _minZ;
            transform.position = correctedPosition;
        }
        if (isAtMaxZ)
        {
            var correctedPosition = transform.position;
            correctedPosition.z = _maxZ;
            transform.position = correctedPosition;
        }
    }

    private void GetIsInMovementBorders(out bool isAtMinX, out bool isAtMaxX, out bool isAtMinZ, out bool isAtMaxZ)
    {
            isAtMinX = false;
            isAtMaxX = false;
            isAtMinZ = false;
            isAtMaxZ = false;

        if (_hasMoveBoundaries)
        {

            if (transform.position.x <= _minX)
            {
                isAtMinX = true;
            }
            if (transform.position.x >= _maxX)
            {
                isAtMaxX = true;
            }
            if (transform.position.z <= _minZ)
            {
                isAtMinZ = true;
            }
            if (transform.position.z >= _maxZ)
            {
                isAtMaxZ = true;
            }
        }
    }
}