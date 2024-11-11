using UnityEditor;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    private const int NumberOfPieces = 100;


    //Добавление во вкладку Tool в Unity Editor кнопку SpawnObject, которая вызывает метод Spawn
    [MenuItem("Tools/SpawnObject")]
    private static void Spawn()
    {
        //Подгрузка с ресурсов префаба для инстанциирования
        GameObject prefab = Resources.Load<GameObject>("BlockPiece");
        //Проверка ссылки префаба на null 
        if (prefab == null)
        {
            Debug.Log("Prefab not found!");
            return;
        }
        //Создание цикла, для создания большого блока состоящего из n маленьких частей
        for (int i = 0; i < NumberOfPieces / 10; i++)
        {
            for (int j = 0; j < NumberOfPieces / 10; j++)
            {
                //Инстанциирование объекта, делим на 20 чтобы инстанциировать блок на 1/20 = 0.05 правее или выше, т.к. размер блока 0.05
                Instantiate(prefab, new Vector2(j / 20f, i / 20f), Quaternion.identity);
            }
        }
        /*Дополнительные ненужные мне возможности
        GameObject instance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        //Имлиментация возможности отмены инстанциирования через Ctrl+Z
        Undo.RegisterCreatedObjectUndo(instance, "Spawned Object");
        //Выделение инстанциированного объекта
        Selection.activeObject = instance;
        */
    }

}
