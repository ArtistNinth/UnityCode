using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class LoginUIController : MonoBehaviour {

	public GameObject startPanel;
	public GameObject loginPanel;
	public GameObject registerPanel;
    public GameObject serverPanel;
	
	
	public Text startUsername;
	
	public InputField loginUsername;
	public InputField loginPassword;
	
	public InputField registerUsername;
	public InputField registerPassword;
	public InputField registerRepassword;
	
	
	private static string username;
	private static string password;
	
	private float miniScale = 0.1f;
	private Vector3 normalScale = new Vector3(1.0f,1.0f,1.0f);
	private float miniTime = 0.1f;

	//start
	public void OnUsernameClick(){
		startPanel.GetComponent<RectTransform>().DOScale(miniScale,miniTime).OnComplete(start2login);
	}
	
	public void OnServerClick(){
        startPanel.GetComponent<RectTransform>().DOScale(miniScale, miniTime).OnComplete(start2server);
	}
	
	public void OnEnterGameClick(){
		//1.连接服务器，验证用户名和密码
		//todo::
		
		//2.进入角色选择界面
		//todo::
	}
	
	private void start2login(){
		startPanel.SetActive(false);
		loginPanel.SetActive(true);
		
		this.showLogin();
	}

    private void start2server() {
        startPanel.SetActive(false);
        serverPanel.SetActive(true);

        this.showServer();
    }
	
	private void showStart(){
		startUsername.text = username;
		
		RectTransform startRectTransform = startPanel.GetComponent<RectTransform>();
		startRectTransform.localScale = normalScale;
		startRectTransform.DOScale(miniScale,miniTime).From();
	}
	
	
	//login
	public void OnLoginClick(){
		//得到用户名和密码并储存起来
		username = loginUsername.text;
		password = loginPassword.text;
		
		//返回start界面
		loginPanel.GetComponent<RectTransform>().DOScale(miniScale,miniTime).OnComplete(login2start);
	}
	
	public void OnRegisterShowClick(){
		loginPanel.GetComponent<RectTransform>().DOScale(miniScale,miniTime).OnComplete(login2regitser);
	}
	
	public void OnLoginCloseClick(){
		loginPanel.GetComponent<RectTransform>().DOScale(miniScale,miniTime).OnComplete(login2start);
	}
	
	private void login2start(){
		loginPanel.SetActive(false);
		startPanel.SetActive(true);		
		
		this.showStart();
	}
	
	private void login2regitser(){
		loginPanel.SetActive(false);
		registerPanel.SetActive(true);
		
		this.showRegister();
	}
	
	private void showLogin(){
		RectTransform loginRectTransform = loginPanel.GetComponent<RectTransform>();
		loginRectTransform.localScale = normalScale;
		loginRectTransform.DOScale(miniScale,miniTime).From();
	}
	
	
	//register
	public void OnRegisterCancelClick(){
		registerPanel.GetComponent<RectTransform>().DOScale(miniScale,miniTime).OnComplete(register2login);
	}
	
	public void OnRegisterLoginClick(){
		//1.本地校验，连接服务器进行验证
		//todo::
		//2.连接失败
		//todo::
		//3.连接成功
		username = registerUsername.text;
		password = registerPassword.text;
		
		registerPanel.GetComponent<RectTransform>().DOScale(miniScale,miniTime).OnComplete(register2start);

	}
	
	public void OnRegisterCloseClick(){
		OnRegisterCancelClick();
	}
	
	private void register2login(){
		registerPanel.SetActive(false);
		loginPanel.SetActive(true);
		
		this.showLogin();
	}
	
	private void register2start(){
		registerPanel.SetActive(false);
		startPanel.SetActive(true);
		
		this.showStart();
	}
	
	private void showRegister(){
		RectTransform registerRectTransform = registerPanel.GetComponent<RectTransform>();
		registerRectTransform.localScale = normalScale;
		registerRectTransform.DOScale(miniScale,miniTime).From();
	}

    //server
    public void OnServerCloseClick() {
        serverPanel.GetComponent<RectTransform>().DOScale(miniScale, miniTime).OnComplete(server2start);
    }

    private void server2start() {
        serverPanel.SetActive(false);
        startPanel.SetActive(true);

        this.showStart();
    }

    private void showServer() {
        RectTransform serverRectTransform = serverPanel.GetComponent<RectTransform>();
        serverRectTransform.localScale = normalScale;
        serverRectTransform.DOScale(miniScale, miniTime).From();
    }
}
