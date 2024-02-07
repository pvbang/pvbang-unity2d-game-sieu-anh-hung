using UnityEngine;
using UnityEngine.SceneManagement;

// list scene hiện có
public enum ListScene
{
    LoadScene,
    ChooseCharacterScene,
    MainScene,
    ArenaScene,
    ListTeamScene,
    PKScene,
    TeamScene,
    WeaponsScene
}

public class LoadScene : BaseButton
{
    public ListScene selectScene;
    public bool isActive = true;
    public bool isLoadLastScene = false;

    // hàm load scene nếu isActive = true
    public void Load()
    {
        if (isActive)
        {
            // nếu isLoadLastScene = true thì load scene trước đó
            if (isLoadLastScene)
            {
                LoadLastScene();
            } else
            {
                SceneManager.LoadScene(selectScene.ToString());
            }
        }  
    }

    // hàm này load nếu được gọi từ hàm khác trong trường hợp isActive = false
    public void LoadSelectScene()
    {
        SceneManager.LoadScene(selectScene.ToString());
    }

    // load scene trước đó (quay lại scene trước đó - last scene)
    public void LoadLastScene()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("LastScene"));
    }

    // reload scene hiện tại
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    protected override void OnClick()
    {
        if (GetComponent<ButtonAnimation>())
        {
            // nêu có animation thì delay theo thời gian của animation
            StartCoroutine(CoroutineHelper.DelaySeconds(() => Load(), GetComponent<ButtonAnimation>().GetLengthAnimation()));
        }
        else
        {
            Load();
        }
    }
}
