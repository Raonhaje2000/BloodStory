using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldMonsterGenManager : MonoBehaviour
{
    GameObject monsters;

    GameObject bacteriaAureus;     // Ȳ�������󱸱� (�����)
    GameObject bacteriaPneumoniae; // ��ű��� (������)
    GameObject bacteriaTetanus;    // �Ļ�ǳ�� (�Ķ���)
    GameObject bacteriaAeruginosa; // ���� (�ʷϻ�)

    [SerializeField]
    GameObject virusRhinovirusPrefab; // ������̷��� (���)

    public Transform bacteriaSpawnPoint; // ������ ���� ���� ����Ʈ
    GameObject[] virusSpawnPoints;       // ���̷����� ���� ���� ����Ʈ��

    int virusSpawnCountMax;     // ���̷����� ������ �ִ� ���� ��
    int virusSpawnCountCurrent; // ���̷����� ������ ���� ���� ��

    bool isVirusSpawnCoolTime;     // ���̷����� ���� ���� ��� �ð�
    WaitForSeconds virusSpawnTime; // ���̷����� ���� ���� �ڷ�ƾ �ð�

    // ������ ���Ͱ� ó�� �����Ǿ����� Ȯ���ϴ� �÷���
    // ó�� �����ÿ��� ���� ��� �ð� ���Ŀ� ������, ���ĺ��� �ٷ� �����ϱ� ����
    bool isAureusFirstSpawn;
    bool isPneumoniaeFirstSpawn;
    bool isTetanusFirstSpawn;
    bool isAeruginosaFirstSpawn;

    private void Awake()
    {
        // ���� ������Ʈ�� �ҷ�����
        LoadObjects();
    }

    void Start()
    {
        // �ʱ�ȭ
        Initialize();
    }

    void Update()
    {
        // ������ ������ Ȱ��ȭ ���� üũ
        CheckFieldBacteriaMonsterActive();

        // ���̷����� ���� ����
        SpawnVirusRhinovirus();
    }

    // ���� ������Ʈ�� �ҷ�����
    void LoadObjects()
    {
        monsters = GameObject.Find("Monsters");

        bacteriaAureus = GameObject.Find("Bacteria_Aureus");
        bacteriaPneumoniae = GameObject.Find("Bacteria_Pneumoniae");
        bacteriaTetanus = GameObject.Find("Bacteria_Tetanus");
        bacteriaAeruginosa = GameObject.Find("Bacteria_Aeruginosa");

        virusRhinovirusPrefab = Resources.Load<GameObject>("FieldMonsterPrefabs/Prefabs/Monster/Virus_Rhinovirus");

        bacteriaSpawnPoint = GameObject.Find("BacteriaSpawnPoint").transform;
        virusSpawnPoints = GameObject.FindGameObjectsWithTag("VirusSpawnPoint");
    }

    // �ʱ�ȭ
    void Initialize()
    {
        bacteriaAureus.transform.position = bacteriaSpawnPoint.position;
        bacteriaPneumoniae.transform.position = bacteriaSpawnPoint.position;
        bacteriaTetanus.transform.position = bacteriaSpawnPoint.position;
        bacteriaAeruginosa.transform.position = bacteriaSpawnPoint.position;

        bacteriaAureus.SetActive(false);
        bacteriaPneumoniae.SetActive(false);
        bacteriaTetanus.SetActive(false);
        bacteriaAeruginosa.SetActive(false);

        virusSpawnCountMax = 20;
        virusSpawnCountCurrent = 0;

        isVirusSpawnCoolTime = false;
        virusSpawnTime = new WaitForSeconds(10.0f);

        isAureusFirstSpawn = true;
        isPneumoniaeFirstSpawn = true;
        isTetanusFirstSpawn = true;
        isAeruginosaFirstSpawn = true;

        // ���� ù ����
        SpawnFirst();
    }

    // ������ ������ Ȱ��ȭ ���� üũ
    void CheckFieldBacteriaMonsterActive()
    {
        // �ش� ���Ͱ� ��Ȱ��ȭ �����̰�, ù ������ �ƴ� ��� �ش� ���� ��� ���� (Ȱ��ȭ)
        if (!bacteriaAureus.activeSelf && !isAureusFirstSpawn) SpawnBacteriaAureus();
        if (!bacteriaPneumoniae.activeSelf && !isPneumoniaeFirstSpawn) SpawnBacteriaPneumoniae();                                       
        if (!bacteriaTetanus.activeSelf && !isTetanusFirstSpawn) SpawnBacteriaTetanus();
        if (!bacteriaAeruginosa.activeSelf && !isAeruginosaFirstSpawn) SpawnBacteriaAeruginosa();
    }

    // ���� ù ����
    void SpawnFirst()
    {
        // ���ͺ��� �ڷ�ƾ�� ���� �� ������ ���� ��� �ð� ���� ����                                              
        StartCoroutine(WaitSpawnAureus());
        StartCoroutine(WaitSpawnPneumoniae());
        StartCoroutine(WaitSpawnTetanus());
        StartCoroutine(WaitSpawnAeruginosa());

        // ���̷����� ���� ����
        SpawnVirusRhinovirus();
    }

    // Ȳ�������󱸱� (�����) ����
    void SpawnBacteriaAureus()
    {
        // ������ ���� ���� ����Ʈ�� �̵���Ų �� �ش� ���� Ȱ��ȭ
        bacteriaAureus.transform.position = bacteriaSpawnPoint.position;                                                       
        bacteriaAureus.SetActive(true);
    }

    // ��ű��� (������) ����
    void SpawnBacteriaPneumoniae()
    {
        // ������ ���� ���� ����Ʈ�� �̵���Ų �� �ش� ���� Ȱ��ȭ
        bacteriaPneumoniae.transform.position = bacteriaSpawnPoint.position;
        bacteriaPneumoniae.SetActive(true);
    }

    // �Ļ�ǳ�� (�Ķ���) ����
    void SpawnBacteriaTetanus()
    {
        // ������ ���� ���� ����Ʈ�� �̵���Ų �� �ش� ���� Ȱ��ȭ
        bacteriaTetanus.transform.position = bacteriaSpawnPoint.position;
        bacteriaTetanus.SetActive(true);
    }

    // ���� (�ʷϻ�) ����
    void SpawnBacteriaAeruginosa()
    {
        // ������ ���� ���� ����Ʈ�� �̵���Ų �� �ش� ���� Ȱ��ȭ
        bacteriaAeruginosa.transform.position = bacteriaSpawnPoint.position;
        bacteriaAeruginosa.SetActive(true);
    }

    // ���̷����� ���� ����
    void SpawnVirusRhinovirus()
    {
        // ���̷����� ���� ���� ���� ���� �ִ� ���� �� ���� ����, ���̷����� ���� ���� ��Ÿ���� �ƴ� ��
        if (virusSpawnCountCurrent < virusSpawnCountMax && !isVirusSpawnCoolTime)
        {
            // ���̷����� ���� ���� ����Ʈ �� �� ���� �������� ����
            int index = Random.Range(0, virusSpawnPoints.Length);

            // ���̷����� ���� ���� �� �θ� ������Ʈ�� Monsters�� ����
            // Monsters ������ ���������ν� ������ �����Ǵ� ������Ʈ�� ������ ����
            GameObject rhino = Instantiate(virusRhinovirusPrefab, virusSpawnPoints[index].transform.position, virusSpawnPoints[index].transform.rotation);
            rhino.transform.SetParent(monsters.transform);

            // ���̷����� ���� ���� ���� �� ����
            virusSpawnCountCurrent++;

            // ���̷����� ���� ���� ��Ÿ���� ���������� ���
            StartCoroutine(WaitSpawnCoolTime());
        }
    }

    // ���̷����� ���� ���� ��Ÿ���� ���������� ���
    IEnumerator WaitSpawnCoolTime()
    {
        if(!isVirusSpawnCoolTime)
        {
            isVirusSpawnCoolTime = true;

            // ���̷����� ���� ����
            SpawnVirusRhinovirus();

            yield return virusSpawnTime;

            isVirusSpawnCoolTime = false;
        }
    }

    // Ȳ�������󱸱� (�����) ���� ���
    IEnumerator WaitSpawnAureus()
    {
        // �ʵ� ���� �� 16�� �ڿ� ����
        if (isAureusFirstSpawn)
        {
            yield return new WaitForSeconds(16.0f);                                                          

            SpawnBacteriaAureus();
            isAureusFirstSpawn = false;
        }
    }

    // ��ű��� (������) ���� ���
    IEnumerator WaitSpawnPneumoniae()
    {
        // �ʵ� ���� �� 1�� �ڿ� ����
        if (isPneumoniaeFirstSpawn)
        {
            yield return new WaitForSeconds(1.0f);

            SpawnBacteriaPneumoniae();
            isPneumoniaeFirstSpawn = false;
        }
    }

    // �Ļ�ǳ�� (�Ķ���) ���� ���
    IEnumerator WaitSpawnTetanus()
    {
        // �ʵ� ���� �� 11�� �ڿ� ����
        if (isTetanusFirstSpawn)
        {
            yield return new WaitForSeconds(11.0f);

            SpawnBacteriaTetanus();
            isTetanusFirstSpawn = false;
        }
    }

    // ���� (�ʷϻ�) ���� ���
    IEnumerator WaitSpawnAeruginosa()
    {
        // �ʵ� ���� �� 6�� �ڿ� ����
        if (isAeruginosaFirstSpawn)
        { 
            yield return new WaitForSeconds(6.0f);

            SpawnBacteriaAeruginosa();
            isAeruginosaFirstSpawn = false;
        }
    }

    // ���̷��� ���� óġ
    public void KillVirus()
    {
        // ���̷����� ������ ���� ���� �� ����                                                                        
        virusSpawnCountCurrent--;  
    }
}