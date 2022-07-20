using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class SoKoban_Editor : EditorWindow
{
#if UNITY_EDITOR

    public Texture2D[] icon = new Texture2D[10];
    //아이콘들 입니다.
    public int[] BtnIconNum = new int[144];
    public Button[] btn = new Button[144];
    //버튼 정보입니다 보통 구조체나 클래스로 만들어도 되는데 규모가 작기 때문에 변수로 만들었습니다

    public int currentLevel = 0;
    //현재 레벨

    public int selectint = 0;
    //타일버튼을 선택한 인덱스

    public BaseField<int> levelField;
    //정수필드

    [MenuItem("Tools/sokoban_Editor")]
    public static void ShowExample()
    {
        SoKoban_Editor wnd = GetWindow<SoKoban_Editor>();
        //불러옵니다.
        wnd.titleContent = new GUIContent("sokoban Editor");
        Vector2 size = new Vector2(750f,850f);
        wnd.minSize = size;
        wnd.maxSize = size;
        //사이즈 설정


    }

    public void CreateGUI()
    {
        icon[0] = null;
        icon[1] = Resources.Load<Texture2D>("wall");
        icon[2] = Resources.Load<Texture2D>("go");
        icon[3] = Resources.Load<Texture2D>("box");
        icon[4] = Resources.Load<Texture2D>("player");
        icon[5] = Resources.Load<Texture2D>("boxAndGo");
        icon[6] = Resources.Load<Texture2D>("playerAndGo");
        //아이콘들을 불러옵니다.
        

        VisualTreeAsset visualTree = (VisualTreeAsset)EditorGUIUtility.Load("Sokoban_Editor.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        rootVisualElement.Add(labelFromUXML);
        var styleSheet = (StyleSheet)EditorGUIUtility.Load("Sokoban_Editor.uss");
        rootVisualElement.styleSheets.Add(styleSheet);
        
        //EditorGUIUtility.Load는 Editor Default Resources폴더안에있는 파일들을 불러옵니다.
        //UXML 와 USS 를 불러옵니다.


        levelField = rootVisualElement.Q<BaseField<int>>("IntegerField");
        levelField.RegisterValueChangedCallback((evt) => { LoadBtn(evt.newValue); });
        //정수
        
        
        rootVisualElement.Q<Button>("NewBtn").clicked += NewBtn;
        rootVisualElement.Q<Button>("OpenBtn").clicked += OpenBtn;
        rootVisualElement.Q<Button>("SaveBtn").clicked += SaveBtn;
        //버튼들 클릭시 함수
        
        
        
        rootVisualElement.Q<Button>("HelpBtn_X").clicked += HelpSelect_X;
        rootVisualElement.Q<Button>("HelpBtn_wall").clicked += HelpSelect_wall;
        rootVisualElement.Q<Button>("HelpBtn_go").clicked += HelpSelect_go;
        rootVisualElement.Q<Button>("HelpBtn_box").clicked += HelpSelect_box;
        rootVisualElement.Q<Button>("HelpBtn_player").clicked += HelpSelect_player;
        rootVisualElement.Q<Button>("HelpBtn_boxAndgo").clicked += HelpSelect_boxAndgo;
        rootVisualElement.Q<Button>("HelpBtn_playerAndgo").clicked += HelpSelect_playerAndgo;
        
        
        //타일버튼 클릭 이벤트


        VisualElement test1 = rootVisualElement.Q<VisualElement>("BtnTool");
        //VisualElement "BtnTool"를 불러옵니다
        
        for (int i = 0; i < 144; i++)
        {
            btn[i] = new Button();
            btn[i].style.width = 50;
            btn[i].style.height = 50;
            btn[i].name = i.ToString();
            test1.Add(btn[i]);
            btn[i].clickable.clickedWithEventInfo += BtnClick;
            btn[i].style.backgroundImage = icon[0];
        }
        //버튼들을 생성시키고 초기화 시킵니다.


        LoadBtn(1);
        //맨처음에는 1레벨을 불러옵니다.
    }

    void HelpSelect_X()
    {
        HelpFunc1();
        selectint = 0;
        rootVisualElement.Q<Button>("HelpBtn_X").style.borderBottomWidth = 5;
        rootVisualElement.Q<Button>("HelpBtn_X").style.borderLeftWidth = 5;
        rootVisualElement.Q<Button>("HelpBtn_X").style.borderRightWidth = 5;
        rootVisualElement.Q<Button>("HelpBtn_X").style.borderTopWidth = 5;
    }

    void HelpSelect_wall()
    {
        HelpFunc1();
        selectint = 1;
        rootVisualElement.Q<Button>("HelpBtn_wall").style.borderBottomWidth = 5;
        rootVisualElement.Q<Button>("HelpBtn_wall").style.borderLeftWidth = 5;
        rootVisualElement.Q<Button>("HelpBtn_wall").style.borderRightWidth = 5;
        rootVisualElement.Q<Button>("HelpBtn_wall").style.borderTopWidth = 5;
    }

    void HelpSelect_go()
    {
        HelpFunc1();
        selectint = 2;
        rootVisualElement.Q<Button>("HelpBtn_go").style.borderBottomWidth = 5;
        rootVisualElement.Q<Button>("HelpBtn_go").style.borderLeftWidth = 5;
        rootVisualElement.Q<Button>("HelpBtn_go").style.borderRightWidth = 5;
        rootVisualElement.Q<Button>("HelpBtn_go").style.borderTopWidth = 5;
    }

    void HelpSelect_box()
    {
        HelpFunc1();
        selectint = 3;
        rootVisualElement.Q<Button>("HelpBtn_box").style.borderBottomWidth = 5;
        rootVisualElement.Q<Button>("HelpBtn_box").style.borderLeftWidth = 5;
        rootVisualElement.Q<Button>("HelpBtn_box").style.borderRightWidth = 5;
        rootVisualElement.Q<Button>("HelpBtn_box").style.borderTopWidth = 5;
    }

    void HelpSelect_player()
    {
        HelpFunc1();
        selectint = 4;
        rootVisualElement.Q<Button>("HelpBtn_player").style.borderBottomWidth = 5;
        rootVisualElement.Q<Button>("HelpBtn_player").style.borderLeftWidth = 5;
        rootVisualElement.Q<Button>("HelpBtn_player").style.borderRightWidth = 5;
        rootVisualElement.Q<Button>("HelpBtn_player").style.borderTopWidth = 5;
    }

    void HelpSelect_boxAndgo()
    {
        HelpFunc1();
        selectint = 5;
        rootVisualElement.Q<Button>("HelpBtn_boxAndgo").style.borderBottomWidth = 5;
        rootVisualElement.Q<Button>("HelpBtn_boxAndgo").style.borderLeftWidth = 5;
        rootVisualElement.Q<Button>("HelpBtn_boxAndgo").style.borderRightWidth = 5;
        rootVisualElement.Q<Button>("HelpBtn_boxAndgo").style.borderTopWidth = 5;
    }

    void HelpSelect_playerAndgo()
    {
        HelpFunc1();
        selectint = 6;
        rootVisualElement.Q<Button>("HelpBtn_playerAndgo").style.borderBottomWidth = 5;
        rootVisualElement.Q<Button>("HelpBtn_playerAndgo").style.borderLeftWidth = 5;
        rootVisualElement.Q<Button>("HelpBtn_playerAndgo").style.borderRightWidth = 5;
        rootVisualElement.Q<Button>("HelpBtn_playerAndgo").style.borderTopWidth = 5;
    }

    void HelpFunc1()
    {
        rootVisualElement.Q<Button>("HelpBtn_X").style.borderBottomWidth = 0;
        rootVisualElement.Q<Button>("HelpBtn_X").style.borderLeftWidth = 0;
        rootVisualElement.Q<Button>("HelpBtn_X").style.borderRightWidth = 0;
        rootVisualElement.Q<Button>("HelpBtn_X").style.borderTopWidth = 0;

        rootVisualElement.Q<Button>("HelpBtn_wall").style.borderBottomWidth = 0;
        rootVisualElement.Q<Button>("HelpBtn_wall").style.borderLeftWidth = 0;
        rootVisualElement.Q<Button>("HelpBtn_wall").style.borderRightWidth = 0;
        rootVisualElement.Q<Button>("HelpBtn_wall").style.borderTopWidth = 0;

        rootVisualElement.Q<Button>("HelpBtn_go").style.borderBottomWidth = 0;
        rootVisualElement.Q<Button>("HelpBtn_go").style.borderLeftWidth = 0;
        rootVisualElement.Q<Button>("HelpBtn_go").style.borderRightWidth = 0;
        rootVisualElement.Q<Button>("HelpBtn_go").style.borderTopWidth = 0;

        rootVisualElement.Q<Button>("HelpBtn_go").style.borderBottomWidth = 0;
        rootVisualElement.Q<Button>("HelpBtn_go").style.borderLeftWidth = 0;
        rootVisualElement.Q<Button>("HelpBtn_go").style.borderRightWidth = 0;
        rootVisualElement.Q<Button>("HelpBtn_go").style.borderTopWidth = 0;

        rootVisualElement.Q<Button>("HelpBtn_box").style.borderBottomWidth = 0;
        rootVisualElement.Q<Button>("HelpBtn_box").style.borderLeftWidth = 0;
        rootVisualElement.Q<Button>("HelpBtn_box").style.borderRightWidth = 0;
        rootVisualElement.Q<Button>("HelpBtn_box").style.borderTopWidth = 0;

        rootVisualElement.Q<Button>("HelpBtn_player").style.borderBottomWidth = 0;
        rootVisualElement.Q<Button>("HelpBtn_player").style.borderLeftWidth = 0;
        rootVisualElement.Q<Button>("HelpBtn_player").style.borderRightWidth = 0;
        rootVisualElement.Q<Button>("HelpBtn_player").style.borderTopWidth = 0;

        rootVisualElement.Q<Button>("HelpBtn_boxAndgo").style.borderBottomWidth = 0;
        rootVisualElement.Q<Button>("HelpBtn_boxAndgo").style.borderLeftWidth = 0;
        rootVisualElement.Q<Button>("HelpBtn_boxAndgo").style.borderRightWidth = 0;
        rootVisualElement.Q<Button>("HelpBtn_boxAndgo").style.borderTopWidth = 0;

        rootVisualElement.Q<Button>("HelpBtn_playerAndgo").style.borderBottomWidth = 0;
        rootVisualElement.Q<Button>("HelpBtn_playerAndgo").style.borderLeftWidth = 0;
        rootVisualElement.Q<Button>("HelpBtn_playerAndgo").style.borderRightWidth = 0;
        rootVisualElement.Q<Button>("HelpBtn_playerAndgo").style.borderTopWidth = 0;
    }

    private void BtnClick(EventBase obj)
    {
        var button = obj.target as Button;
        int idx = int.Parse(button.name);
        BtnIconNum[idx] = selectint;
        button.style.backgroundImage = icon[BtnIconNum[idx]];
    }

    public void NewBtn()
    {
        rootVisualElement.Q<Label>("Text").text = "";
        int check = 0;
        while (true)
        {
            check++;
            if (!Resources.Load("Maps/Lv" + check))
            {
                break;
            }
        }

        currentLevel = check;
        levelField.value = currentLevel;
        for (int i = 0; i < 144; i++)
        {
            BtnIconNum[i] = 0;
            btn[i].style.backgroundImage = icon[BtnIconNum[i]];
        }
    }

    public void OpenBtn()
    {
        var path = EditorUtility.OpenFilePanel("Open level", Application.dataPath + "/Resources/Maps",
            "csv");
        string filename = Path.GetFileName(path);
        if (!string.IsNullOrEmpty(path))
        {
            currentLevel = int.Parse(filename.Substring(2, filename.Length - 6));
            LoadBtn(currentLevel);
            levelField.value = currentLevel;
        }
    }

    public void SaveBtn()
    {
        int playercnt = 0;
        int boxcnt = 0;
        int gocnt = 0;
        for (int i = 0; i < 144; i++)
        {
            if (BtnIconNum[i] == 2)
            {
                gocnt++;
            }
            else if (BtnIconNum[i] == 3)
            {
                boxcnt++;
            }
            else if (BtnIconNum[i] == 4)
            {
                playercnt++;
            }
            else if (BtnIconNum[i] == 5)
            {
                boxcnt++;
                gocnt++;
            }
            else if (BtnIconNum[i] == 6)
            {
                gocnt++;
                playercnt++;
            }
        }

        if (playercnt != 1)
        {
            rootVisualElement.Q<Label>("Text").text = "플레이어는 무조건 하나만 있어야합니다.";
            return;
        }

        if (boxcnt == 0)
        {
            rootVisualElement.Q<Label>("Text").text = "박스는 최소 하나있어야합니다.";
            return;
        }

        if (boxcnt != gocnt)
        {
            rootVisualElement.Q<Label>("Text").text = "박스를 집어넣는 공간과 갯수를 똑같이 맞춰주세요";
            return;
        }

        StreamWriter streamSave =
            new StreamWriter(Application.dataPath + "/Resources/Maps/Lv" + currentLevel.ToString() + ".csv");
        streamSave.WriteLine("0,1,2,3,4,5,6,7,8,9,10,11");


        int cnt = 0;
        for (int i = 0; i < 12; i++)
        {
            string s = null;
            for (int j = 0; j < 12; j++)
            {
                if (j < 11)
                {
                    s += BtnIconNum[cnt] + ",";
                }
                else
                {
                    s += BtnIconNum[cnt];
                }

                cnt++;
            }

            streamSave.WriteLine(s);
        }

        streamSave.Flush();
        streamSave.Close();

        rootVisualElement.Q<Label>("Text").text = "저장성공";
        AssetDatabase.Refresh();
    }

    public void LoadBtn(int Lv)
    {
        rootVisualElement.Q<Label>("Text").text = "";
        if (!Resources.Load("Maps/Lv" + Lv))
        {
            for (int i = 0; i < 144; i++)
            {
                BtnIconNum[i] = 0;
                btn[i].style.backgroundImage = icon[BtnIconNum[i]];
            }

            return;
        }

        currentLevel = Lv;
        string path = "Maps/Lv" + Lv;
        var data = CSVReader.Read(path);


        int cnt = 0;
        for (int i = 0; i < 12; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                int dataSet = (int)data[i][j.ToString()];
                BtnIconNum[cnt] = dataSet;
                btn[cnt].style.backgroundImage = icon[BtnIconNum[cnt]];
                cnt++;
            }
        }
    }


#endif
}