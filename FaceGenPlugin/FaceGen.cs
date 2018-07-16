using System;
using Character;
using FaceGenPlugin.Util;
using FkAssistPlugin;
using Unity.Linq;
using UnityEngine;
using Random = System.Random;

namespace FaceGenPlugin
{
    public class FaceGen : BaseMgr<FaceGen>
    {
        private bool _show;

        private int wid = 17539;
        private int range = 0;
        private static readonly int LEN = SEXY.CharDefine.cf_headshapename.Length;
        private float[] store = new float[LEN];
        private bool[] enabled = new bool[LEN];
        private int[] typePos = new int[] {0, 5, 13, 19, 24, 37, 40, 55, 62, 67};

        private string[] typeName = new string[]
        {
            "颜", "颚", "颊", "眉",
            "目", "瞳", "鼻", "口", "耳"
        };

        public override void Init()
        {
            Tracer.Log("FaceGen Init");
            var human = FindObjectOfType<Human>();
            if (human != null && human.sex == SEX.FEMALE)
            {
                for (int i = 0; i < LEN; i++)
                {
                    store[i] = human.head.GetShape(i);
                    enabled[i] = true;
                }
            }
        }

        private void OnGUI()
        {
            _show = GUI.Toggle(new Rect(0, 0, 20, 20), _show, "");
            if (_show)
            {
                GUI.Window(wid, GUIX.DefaultRect(), ShowWindow, "我的窗口");
            }
        }

        private void ShowWindow(int id)
        {
            try
            {
                if (Event.current.type == EventType.MouseDown)
                {
                    GUI.FocusWindow(wid);

                    CamCtrl.windowdragflag = true;
                }
                else if (Event.current.type == EventType.MouseUp)
                {
                    CamCtrl.windowdragflag = false;
                }
                var human = FindObjectOfType<Human>();
                if (human != null && human.sex == SEX.FEMALE)
                {
                    GUIX.BeginHorizontal();
                    if (GUIX.Button("store"))
                    {
                        for (int i = 0; i < LEN; i++)
                        {
                            store[i] = human.head.GetShape(i);
                        }
                    }
                    if (GUIX.Button("reset"))
                    {
                        for (int i = 0; i < LEN; i++)
                        {
                            human.head.SetShape(i, store[i]);
                        }
                    }
                    range = GUIX.Range(range);
                    if (GUIX.Button("random"))
                    {
                        var r = new Random();
                        for (int i = 0; i < LEN; i++)
                        {
                            if (enabled[i])
                            {
                                var delta = r.Next(-range, range);
                                var v = store[i] + delta / 100.0f;
                                human.head.SetShape(i, v);
                            }
                        }
                    }
                    GUIX.EndHorizontal();

                    GUIX.BeginScrollView();
                    var typeIdx = 0;
                    for (int i = 0; i < LEN; i++)
                    {
                        if (i == typePos[typeIdx])
                        {
                            GUIX.BeginHorizontal();
                            GUIX.Label(typeName[typeIdx]);
                            var allEnabled = true;
                            for (int j = i; j < typePos[typeIdx + 1]; j++)
                            {
                                if (enabled[j] == false)
                                {
                                    allEnabled = false;
                                }
                            }
                            var res = GUIX.Toggle(allEnabled, "");
                            if (res != allEnabled)
                            {
                                for (int j = i; j < typePos[typeIdx + 1]; j++)
                                {
                                    enabled[j] = res;
                                }
                            }
                            GUIX.EndHorizontal();
                            typeIdx++;
                        }
                        GUIX.BeginHorizontal();
                        var key = SEXY.CharDefine.cf_headshapename[i];
                        var value = human.head.GetShape(i) * 100;
                        GUIX.Label((i + 1).ToString());
                        GUIX.Label(key, 2);
                        GUIX.Label(value.ToString("0"));
                        if (GUIX.Button("<<"))
                        {
                            value -= 10.0f;
                            human.head.SetShape(i, value / 100.0f);
                        }
                        if (GUIX.Button("<"))
                        {
                            value -= 1.0f;
                            human.head.SetShape(i, value / 100.0f);
                        }
                        if (GUIX.Button(">"))
                        {
                            value += 1.0f;
                            human.head.SetShape(i, value / 100.0f);
                        }
                        if (GUIX.Button(">>"))
                        {
                            value += 10.0f;
                            human.head.SetShape(i, value / 100.0f);
                        }
                        enabled[i] = GUIX.Toggle(enabled[i], "");

                        GUIX.EndHorizontal();
                    }
                    GUIX.EndScrollView();
                }
            }
            catch (Exception e)
            {
                Tracer.Log(e);
            }
        }

        private void Update()
        {
            try
            {
                InnerUpdate();
            }
            catch (Exception e)
            {
                Tracer.Log(e);
            }
        }

        private void InnerUpdate()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                _show = !_show;
            }
        }
    }
}