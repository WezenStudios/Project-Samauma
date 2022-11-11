/// Código desenvolvido por: Nicholas M. França

using UnityEngine;
using TMPro;

[RequireComponent (typeof (TextMeshProUGUI))]
public class TextTyperTMP : MonoBehaviour 
{	
	[Header("Ajustes:")]
	[SerializeField] private float typeSpeed		= 0.1f;
	[SerializeField] private float startDelay 		= 0.5f;
	[SerializeField] private float volumeVariation 	= 0.1f;
	[SerializeField] private bool startOnStart		= false;

	[Header("Componentes:")]
	[SerializeField] private AudioSource mainAudioSource;

	private bool typing;
	private int counter;
	private string textToType;
	private TextMeshProUGUI textComponent;

	private void Awake()
	{
		textComponent = GetComponent<TextMeshProUGUI>();

		if(!mainAudioSource)
		{
			Debug.Log("Sem AudioSource associado. Coloque um se quiser utilizar os efeitos sonoros de digitação!");
		}
		
		counter = 0;
		textToType = textComponent.text;
		textComponent.text = "";
	}

	private void Start()
	{
		if(startOnStart)
		{
            StartTyping();
		}
	}

	public void StartTyping()
	{	
		if(!typing)
		{
			InvokeRepeating("Type", startDelay, typeSpeed);
		}
		else
		{
			Debug.LogWarning(gameObject.name + " : Já está digitando!");
		}
	}

	public void StopTyping()
	{
        counter = 0;
        typing = false;
		CancelInvoke("Type");
	}

    public void UpdateText(string newText)
    {   
        StopTyping();
        textComponent.text = "";
        textToType = newText;
        StartTyping();
    }

	public void QuickSkip()
	{
		if(typing)
		{
			StopTyping();
			textComponent.text = textToType;
		}
	}

	private void Type()
	{	
		typing = true;
		textComponent.text = textComponent.text + textToType[counter];
		counter++;

		if(mainAudioSource)
		{	
			mainAudioSource.Play();
			RandomiseVolume();
		}

		if(counter == textToType.Length)
		{	
			typing = false;
			CancelInvoke("Type");
		}
	}

	private void RandomiseVolume()
	{
		mainAudioSource.volume = Random.Range(1 - volumeVariation, volumeVariation + 1);
	}

    public bool IsTyping() { return typing; }
}

/*
Copyright 2022 Nicholas Macêdo França

É concedida a permissão, gratuitamente, a qualquer pessoa que obtenha uma cópia deste código e dos arquivos de documentação associados (o "Software / Código Fonte"), para lidar com 
ele sem restrição, incluindo, sem limitação, os direitos de usar, copiar, modificar, distribuir, e permitir que as pessoas a quem o Software é fornecido o façam, 
sujeito às seguintes condições:

O aviso de direitos autorais acima e este aviso de permissão devem ser incluídos em todas as cópias ou partes substanciais do projeto.

O SOFTWARE É FORNECIDO "COMO ESTÁ", SEM GARANTIA DE QUALQUER TIPO, EXPRESSA OU IMPLÍCITA, INCLUINDO, MAS NÃO SE LIMITANDO ÀS GARANTIAS DE
COMERCIALIZAÇÃO, ADEQUAÇÃO A UM DETERMINADO FIM E NÃO VIOLAÇÃO. EM NENHUMA CIRCUNSTÂNCIA OS AUTORES OU DETENTORES DE DIREITOS AUTORAIS SERÃO RESPONSÁVEIS POR
QUALQUER REIVINDICAÇÃO, DANOS OU OUTRA RESPONSABILIDADE, SEJA EM UMA AÇÃO DE CONTRATO, ATO ILÍCITO OU DE OUTRA FORMA, DECORRENTE DE, OU EM CONEXÃO COM O SOFTWARE.
 */