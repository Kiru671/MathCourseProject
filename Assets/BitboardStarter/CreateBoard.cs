using System;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateBoard : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public GameObject housePrefab;
    public GameObject treePrefab;
    public TextMeshProUGUI score;
    private GameObject[] tiles;
    long dirtBitboard;
    long desertBitboard;
    long treeBitBoard;
    long playerBitBoard;
    void Start()
    {
        tiles = new GameObject[64];
        for (int r = 0; r < 8; r++)
        {
            for (int c = 0; c < 8; c++)
            {
                int randomTile = UnityEngine.Random.Range(0, tilePrefabs.Length);
                Vector3 pos = new Vector3(c, 0, r);
                GameObject tile = Instantiate(tilePrefabs[randomTile], pos, Quaternion.identity);
                tiles[r * 8 + c] = tile;
                tile.name = tile.tag + "_" + r + "_" + c;
                if (tile.tag == "Dirt")
                {
                    dirtBitboard = SetCellState(dirtBitboard, r, c);
                }
                if (tile.tag == "Desert")
                {
                    desertBitboard = SetCellState(desertBitboard, r, c);
                }
            }
        }
        Debug.Log("Dirt cells = "+ CellCount(dirtBitboard));
        InvokeRepeating("PlantTree",1,1);
    }

    void PlantTree()
    {
        Debug.Log("Attempting to plant tree");
        int rr = UnityEngine.Random.Range(0, 8);
        int rc = UnityEngine.Random.Range(0, 8);
        if (GetCellState(dirtBitboard & ~playerBitBoard, rr, rc))
        {
            var tree = Instantiate(treePrefab, tiles[rr * 8 + rc].transform, true);
            tree.transform.localPosition = Vector3.zero;
            treeBitBoard = SetCellState(treeBitBoard, rr, rc);
        }
    }

    // Visualize tile placement in the whole bitboard as it is placed on Start()
    
    void PrintBB(string name, long BitBoard)
    {
        Debug.Log(name + ": " + Convert.ToString(BitBoard,2).PadLeft(64,'0'));
    }

    // Add tile to bitboard as a 1.
    
    long SetCellState(long bitboard, int row, int col)
    {
        long newBit =  1L << (row * 8 + col);
        return (bitboard |= newBit);
    }

    bool GetCellState(long bitboard, int row, int col)
    {
        long mask = 1L <<(row * 8 + col);
        return ((bitboard & mask) != 0);
    }

    void CalculateScore()
    {
        score.text = string.Format("Score: {0}", CellCount(playerBitBoard & dirtBitboard) * 10 + 
                                                 CellCount(playerBitBoard & desertBitboard) * 2);
    }
    
    // Get how many cells in given bitboard are set to 1 (i.e. how many tiles are in the bitboard)
    int CellCount(long bitboard)
    {
        int count = 0;
        long bb = bitboard;
        while (bb != 0)
        {
            bb &= bb - 1;
            count++;
        }
        return count;
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit))
            {
                // Check if chosen tile doesn't have dirt, a tree or housing already.
                
                int r = (int)hit.collider.gameObject.transform.position.z;
                int c = (int)hit.collider.gameObject.transform.position.x;

                if (!GetCellState((dirtBitboard & ~treeBitBoard & ~playerBitBoard) | (desertBitboard & ~playerBitBoard) , r, c))
                    return;
                
                // Instantiate and place house on chosen tile.
                
                GameObject house = Instantiate(housePrefab, hit.collider.gameObject.transform, true);
                house.transform.localPosition = Vector3.zero;
                playerBitBoard = SetCellState(playerBitBoard, (int)hit.collider.gameObject.transform.position.z,
                                                            (int)hit.collider.gameObject.transform.position.x);
                
                CalculateScore();
            }
        }
    }
}
