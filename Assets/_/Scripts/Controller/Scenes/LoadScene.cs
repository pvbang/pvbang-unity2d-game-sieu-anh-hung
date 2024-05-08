using UnityEngine;
using UnityEngine.SceneManagement;

// list scene hiện có
public enum ListScene
{
    LoadScene,
    ChooseCharacterScene,
    MainScene,
    ArenaScene,
    BagScene,
    HeroScene,
    PKScene,
    TeamScene,
    WeaponsScene,
    HotScene,
    GachaScene,
    FighterMapScene,
    AllHeroScene,
    MyHeroScene,
}

public class LoadScene : BaseButton
{
    public ListScene selectScene;
    public bool isActive = true;
    public bool isLoadLastScene = false;

    public bool isNameScene = false;
    public string nameScene = "";

    // hàm load scene nếu isActive = true
    public void Load()
    {
        if (isActive)
        {
            // nếu isLoadLastScene = true thì load scene trước đó
            if (isLoadLastScene)
            {
                WaitingController.Instance.StartWaiting();
                LoadLastScene();
            } 
            else if (isNameScene)
            {
                WaitingController.Instance.StartWaiting();
                SceneManager.LoadScene(nameScene);
            }
            else
            {
                WaitingController.Instance.StartWaiting();
                SceneManager.LoadScene(selectScene.ToString());
            }
        }  
    }

    // hàm này load nếu được gọi từ hàm khác trong trường hợp isActive = false
    public void LoadSelectScene()
    {
        if (GetComponent<ButtonAnimation>())
        {
            WaitingController.Instance.StartWaiting();
            // nếu có animation thì delay theo thời gian của animation
            StartCoroutine(CoroutineHelper.DelaySeconds(() => SceneManager.LoadScene(selectScene.ToString()), GetComponent<ButtonAnimation>().GetLengthAnimation()));
        }
        else
        {   
            WaitingController.Instance.StartWaiting();
            SceneManager.LoadScene(selectScene.ToString());
        }
    }

    // load scene trước đó (quay lại scene trước đó - last scene)
    public void LoadLastScene()
    {
        if (GetComponent<ButtonAnimation>())
        {
            WaitingController.Instance.StartWaiting();
            // nếu có animation thì delay theo thời gian của animation
            StartCoroutine(CoroutineHelper.DelaySeconds(() => SceneManager.LoadScene(PlayerPrefs.GetString("LastScene")), GetComponent<ButtonAnimation>().GetLengthAnimation()));
        }
        else
        {
            WaitingController.Instance.StartWaiting();
            SceneManager.LoadScene(PlayerPrefs.GetString("LastScene"));
        }
    }

    // reload scene hiện tại
    public void ReloadScene()
    {
        if (GetComponent<ButtonAnimation>())
        {
            WaitingController.Instance.StartWaiting();
            // nếu có animation thì delay theo thời gian của animation
            StartCoroutine(CoroutineHelper.DelaySeconds(() => SceneManager.LoadScene(SceneManager.GetActiveScene().name), GetComponent<ButtonAnimation>().GetLengthAnimation()));
        }
        else
        {
            WaitingController.Instance.StartWaiting();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    protected override void OnClick()
    {
        if (GetComponent<ButtonAnimation>())
        {
            // nếu có animation thì delay theo thời gian của animation
            StartCoroutine(CoroutineHelper.DelaySeconds(() => Load(), GetComponent<ButtonAnimation>().GetLengthAnimation()));
        }
        else
        {
            Load();
        }
    }
}
