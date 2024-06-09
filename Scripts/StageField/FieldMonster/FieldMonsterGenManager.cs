using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldMonsterGenManager : MonoBehaviour
{
    GameObject monsters;

    GameObject bacteriaAureus;     // 황색포도상구균 (노란색)
    GameObject bacteriaPneumoniae; // 폐렴구균 (빨간색)
    GameObject bacteriaTetanus;    // 파상풍균 (파란색)
    GameObject bacteriaAeruginosa; // 녹농균 (초록색)

    [SerializeField]
    GameObject virusRhinovirusPrefab; // 리노바이러스 (흰색)

    public Transform bacteriaSpawnPoint; // 세균형 몬스터 생성 포인트
    GameObject[] virusSpawnPoints;       // 바이러스형 몬스터 생성 포인트들

    int virusSpawnCountMax;     // 바이러스형 몬스터의 최대 생성 수
    int virusSpawnCountCurrent; // 바이러스형 몬스터의 현재 생성 수

    bool isVirusSpawnCoolTime;     // 바이러스형 몬스터 생성 대기 시간
    WaitForSeconds virusSpawnTime; // 바이러스형 몬스터 생성 코루틴 시간

    // 세균형 몬스터가 처음 생성되었는지 확인하는 플래그
    // 처음 생성시에만 생성 대기 시간 이후에 나오고, 이후부턴 바로 생성하기 위함
    bool isAureusFirstSpawn;
    bool isPneumoniaeFirstSpawn;
    bool isTetanusFirstSpawn;
    bool isAeruginosaFirstSpawn;

    private void Awake()
    {
        // 관련 오브젝트들 불러오기
        LoadObjects();
    }

    void Start()
    {
        // 초기화
        Initialize();
    }

    void Update()
    {
        // 세균형 몬스터의 활성화 상태 체크
        CheckFieldBacteriaMonsterActive();

        // 바이러스형 몬스터 생성
        SpawnVirusRhinovirus();
    }

    // 관련 오브젝트들 불러오기
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

    // 초기화
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

        // 몬스터 첫 생성
        SpawnFirst();
    }

    // 세균형 몬스터의 활성화 상태 체크
    void CheckFieldBacteriaMonsterActive()
    {
        // 해당 몬스터가 비활성화 상태이고, 첫 생성이 아닌 경우 해당 몬스터 즉시 생성 (활성화)
        if (!bacteriaAureus.activeSelf && !isAureusFirstSpawn) SpawnBacteriaAureus();
        if (!bacteriaPneumoniae.activeSelf && !isPneumoniaeFirstSpawn) SpawnBacteriaPneumoniae();                                       
        if (!bacteriaTetanus.activeSelf && !isTetanusFirstSpawn) SpawnBacteriaTetanus();
        if (!bacteriaAeruginosa.activeSelf && !isAeruginosaFirstSpawn) SpawnBacteriaAeruginosa();
    }

    // 몬스터 첫 생성
    void SpawnFirst()
    {
        // 몬스터별로 코루틴을 돌려 각 몬스터의 생성 대기 시간 이후 생성                                              
        StartCoroutine(WaitSpawnAureus());
        StartCoroutine(WaitSpawnPneumoniae());
        StartCoroutine(WaitSpawnTetanus());
        StartCoroutine(WaitSpawnAeruginosa());

        // 바이러스형 몬스터 생성
        SpawnVirusRhinovirus();
    }

    // 황색포도상구균 (노란색) 생성
    void SpawnBacteriaAureus()
    {
        // 세균형 몬스터 생성 포인트로 이동시킨 후 해당 몬스터 활성화
        bacteriaAureus.transform.position = bacteriaSpawnPoint.position;                                                       
        bacteriaAureus.SetActive(true);
    }

    // 폐렴구균 (빨간색) 생성
    void SpawnBacteriaPneumoniae()
    {
        // 세균형 몬스터 생성 포인트로 이동시킨 후 해당 몬스터 활성화
        bacteriaPneumoniae.transform.position = bacteriaSpawnPoint.position;
        bacteriaPneumoniae.SetActive(true);
    }

    // 파상풍균 (파란색) 생성
    void SpawnBacteriaTetanus()
    {
        // 세균형 몬스터 생성 포인트로 이동시킨 후 해당 몬스터 활성화
        bacteriaTetanus.transform.position = bacteriaSpawnPoint.position;
        bacteriaTetanus.SetActive(true);
    }

    // 녹농균 (초록색) 생성
    void SpawnBacteriaAeruginosa()
    {
        // 세균형 몬스터 생성 포인트로 이동시킨 후 해당 몬스터 활성화
        bacteriaAeruginosa.transform.position = bacteriaSpawnPoint.position;
        bacteriaAeruginosa.SetActive(true);
    }

    // 바이러스형 몬스터 생성
    void SpawnVirusRhinovirus()
    {
        // 바이러스형 몬스터 현재 생성 수가 최대 생성 수 보다 적고, 바이러스형 몬스터 생성 쿨타임이 아닐 때
        if (virusSpawnCountCurrent < virusSpawnCountMax && !isVirusSpawnCoolTime)
        {
            // 바이러스형 몬스터 생성 포인트 중 한 곳을 랜덤으로 선택
            int index = Random.Range(0, virusSpawnPoints.Length);

            // 바이러스형 몬스터 생성 후 부모 오브젝트를 Monsters로 변경
            // Monsters 하위에 생성함으로써 여러개 생성되는 오브젝트들 관리를 위함
            GameObject rhino = Instantiate(virusRhinovirusPrefab, virusSpawnPoints[index].transform.position, virusSpawnPoints[index].transform.rotation);
            rhino.transform.SetParent(monsters.transform);

            // 바이러스형 몬스터 현재 생성 수 증가
            virusSpawnCountCurrent++;

            // 바이러스형 몬스터 생성 쿨타임이 끝날때까지 대기
            StartCoroutine(WaitSpawnCoolTime());
        }
    }

    // 바이러스형 몬스터 생성 쿨타임이 끝날때까지 대기
    IEnumerator WaitSpawnCoolTime()
    {
        if(!isVirusSpawnCoolTime)
        {
            isVirusSpawnCoolTime = true;

            // 바이러스형 몬스터 생성
            SpawnVirusRhinovirus();

            yield return virusSpawnTime;

            isVirusSpawnCoolTime = false;
        }
    }

    // 황색포도상구균 (노란색) 생성 대기
    IEnumerator WaitSpawnAureus()
    {
        // 필드 진입 후 16초 뒤에 생성
        if (isAureusFirstSpawn)
        {
            yield return new WaitForSeconds(16.0f);                                                          

            SpawnBacteriaAureus();
            isAureusFirstSpawn = false;
        }
    }

    // 폐렴구균 (빨간색) 생성 대기
    IEnumerator WaitSpawnPneumoniae()
    {
        // 필드 진입 후 1초 뒤에 생성
        if (isPneumoniaeFirstSpawn)
        {
            yield return new WaitForSeconds(1.0f);

            SpawnBacteriaPneumoniae();
            isPneumoniaeFirstSpawn = false;
        }
    }

    // 파상풍균 (파란색) 생성 대기
    IEnumerator WaitSpawnTetanus()
    {
        // 필드 진입 후 11초 뒤에 생성
        if (isTetanusFirstSpawn)
        {
            yield return new WaitForSeconds(11.0f);

            SpawnBacteriaTetanus();
            isTetanusFirstSpawn = false;
        }
    }

    // 녹농균 (초록색) 생성 대기
    IEnumerator WaitSpawnAeruginosa()
    {
        // 필드 진입 후 6초 뒤에 생성
        if (isAeruginosaFirstSpawn)
        { 
            yield return new WaitForSeconds(6.0f);

            SpawnBacteriaAeruginosa();
            isAeruginosaFirstSpawn = false;
        }
    }

    // 바이러스 몬스터 처치
    public void KillVirus()
    {
        // 바이러스형 몬스터의 현재 생성 수 감소                                                                        
        virusSpawnCountCurrent--;  
    }
}