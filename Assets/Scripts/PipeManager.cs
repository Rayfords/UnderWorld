using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Threading;
using UnityEngine;

namespace KL
{
    public class PipeManager : MonoBehaviour
    {
        string CENTERS_PIPE_NAME = "centers_pipe_wall";
        string CONTOURS_PIPE_NAME = "contours_concave_pipe_wall";

        public static PipeManager Instance;

        private const int MAX_CONTOURS_COUNT = 300;

        private static readonly int PIPE_BUFFER_BYTES = 50000;
        private static readonly int PIPE_BUFFER_FLOATS = PIPE_BUFFER_BYTES / 4;
        private List<Vector2> centers = new List<Vector2>();
        private List<List<Vector2>> contours = new List<List<Vector2>>();
        private bool contoursPipeIsRunning;
        private bool centersPipeIsRunning;
        private bool shouldSend;

        private static int WIDTH;
        private static int HEIGHT;

        private void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            WIDTH = Display.displays[0].renderingWidth;
            HEIGHT = Display.displays[0].renderingHeight;

            contoursPipeIsRunning = false;
            centersPipeIsRunning = false;
            shouldSend = false;
        }

        private void Update()
        {

            if (!contoursPipeIsRunning)
            {
                StartContoursPipe();
                contoursPipeIsRunning = true;
            }

            if (!centersPipeIsRunning)
            {
                StartCentersPipe();
                centersPipeIsRunning = true;
            }
        }

        private void OnDestroy()
        {
            shouldSend = false;
        }

        #region Pipes
        private void StartCentersPipe()
        {
            shouldSend = true;
            Thread pipeCentersThread = new Thread(pipeCentersThreadFunc);
            pipeCentersThread.Start();
        }

        private void StartContoursPipe()
        {
            shouldSend = true;
            Thread pipeContoursThread = new Thread(pipeContoursThreadFunc);
            pipeContoursThread.Start();
        }
        private void pipeCentersThreadFunc()
        {
            try
            {
                using (NamedPipeClientStream centersPipe = new NamedPipeClientStream(".", CENTERS_PIPE_NAME, PipeDirection.In))
                {
                    centersPipe.Connect();
                    while (centersPipe.IsConnected && shouldSend)
                    {
                        byte[] bytes = new byte[PIPE_BUFFER_BYTES];
                        int len = centersPipe.Read(bytes, 0, PIPE_BUFFER_BYTES);

                        SaveCenters(bytes, len);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Log("Centers pipe failed, reconnecting... " + ex.Message);
                Thread.Sleep(5000);
            }

            centersPipeIsRunning = false;
        }

        private void pipeContoursThreadFunc()
        {
            try
            {
                using (NamedPipeClientStream contoursPipe = new NamedPipeClientStream(".", CONTOURS_PIPE_NAME, PipeDirection.In))
                {
                    contoursPipe.Connect();
                    while (contoursPipe.IsConnected && shouldSend)
                    {
                        byte[] bytes = new byte[PIPE_BUFFER_BYTES];
                        int len = contoursPipe.Read(bytes, 0, PIPE_BUFFER_BYTES);
                        SaveContours(bytes, len);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Log("Contours pipe failed, reconnecting..." + ex.Message);
                Thread.Sleep(5000);
            }

            contoursPipeIsRunning = false;
        }
        #endregion

        #region Centers
        private void SaveCenters(byte[] bytes, int len)
        {
            lock (centers)
            {
                centers.Clear();
                float[] floats = new float[len / 4];
                Buffer.BlockCopy(bytes, 0, floats, 0, len);
              
                int length = floats.Length;

                if (length < 1) return;
                int i = 0;

                while (i < length)
                {
                    // flip y coordinate by 1.0f-y
                    centers.Add(new Vector2(
                        floats[i] * WIDTH,
                        (1.0f - floats[i + 1]) * HEIGHT
                    ));



                    i += 2;
                }
            }
        }
        #endregion

        #region Contours
        private void SaveContours(byte[] bytes, int len)
        {
            lock (contours)
            {
                contours.Clear();
                float[] floats = new float[len / 4];
                Buffer.BlockCopy(bytes, 0, floats, 0, len);

                int length = floats.Length;
              
                if (length < 1) return;
                int i = 0;
                int contoursIndex = 0;
                bool readPositions = false;

                while (i < length)
                {
                    if (readPositions)
                    {
                        if (floats[i] == -2)
                        {
                            readPositions = false;
                            contoursIndex++;
                            i++;
                            continue;
                        }
                        //flip y coordinate by 1.0f - y
                        contours[contoursIndex].Add(new Vector2(
                            floats[i] * WIDTH,
                            (1.0f - floats[i + 1]) * HEIGHT
                        ));
                        i += 2;
                    }
                    else
                    {
                        readPositions = true;
                        contours.Add(new List<Vector2>());
                    }
                }


            }
        }
        #endregion

        #region Getters

        public List<Vector2> GetCenters()
        {
            return centers;
        }
        public List<List<Vector2>> GetContours()
        {
            return contours;
        }
        #endregion
    }
}