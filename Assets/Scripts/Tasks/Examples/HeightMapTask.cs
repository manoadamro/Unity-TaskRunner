using UnityEngine;

namespace Tasks.Examples
{
    [RequireComponent(typeof(TaskRunner))]
    public class HeightMapTask : MonoBehaviour
    {
        [SerializeField] private Vector2Int size = new Vector2Int(1000, 1000);

        public void RunTask()
        {
            var taskRunner = GetComponent<TaskRunner>();
            var task = new TaskData<Vector2Int, float[,]>
            {
                Title = $"Generate {size.x}x{size.y} Height Map",
                Action = MyTask,
                Arg1 = size,
                Callback = OnTaskComplete,
                Join = true
            };
            taskRunner.RunTask(task);
        }

        private void OnTaskComplete(float[,] heightMap) => Debug.Log($"Completed Heightmap: {heightMap}");

        // TODO do something with heightmap
        private float[,] MyTask(Vector2Int mapSize, out TaskProgress progress)
        {
            var heightMap = new float[mapSize.x, mapSize.y];
            var total = mapSize.x * mapSize.y;
            var counter = 0f;

            for (var y = 0; y < mapSize.y; y++)
            for (var x = 0; x < mapSize.x; x++)
            {
                heightMap[x, y] = Mathf.PerlinNoise(x, y);
                counter += 1f;
                progress = new TaskProgress(
                    $"Created to {counter} of {total}",
                    counter / total);
            }

            progress = new TaskProgress("Done!", 1f);
            return heightMap;
        }
    }
}
