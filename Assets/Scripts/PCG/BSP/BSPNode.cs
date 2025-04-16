using System.Collections.Generic;
using UnityEngine;


public class BSPNode
{
    private BoundsInt _room;

    private BSPNode _left;
    private BSPNode _right;

    private bool _verticalSliced;// true : Vertical cut, False : Horizontal cut
    public BoundsInt Room => _room;

    
    public BSPNode(int centerX, int centerY, int width, int height, bool slice)
    {
        Vector3Int size = new Vector3Int(width, height, 0);
        Vector3Int position = new Vector3Int(centerX,centerY,0) - size / 2;
        
        _room = new BoundsInt(position, size);
        _verticalSliced = slice;
    }

    private BSPNode(BoundsInt bounds, bool slice)
    {
        _room = bounds;
        _verticalSliced = slice;
    }


    public void Process(List<BoundsInt> rooms, float maxSize)
    {
        if (_room.size.x * _room.size.y <= maxSize)
        {
            rooms.Add(_room);
            return;
        }
        
        
        // Cut into smaller if nedded
        float ratio = Random.Range(0.25f, 0.75f);
        BoundsInt left = new BoundsInt(_room.position, _room.size);
        BoundsInt right = new BoundsInt(_room.position, _room.size);
        
        Vector3 cutPoint = _room.position + new Vector3(ratio * _room.size.x, ratio * _room.size.y, ratio * _room.size.z);
        
        if (_verticalSliced)
        {
            left.xMax = Mathf.RoundToInt(cutPoint.x);
            right.xMin = Mathf.RoundToInt(cutPoint.x);
        }
        else
        {
            left.yMax = Mathf.RoundToInt(cutPoint.y);
            right.yMin = Mathf.RoundToInt(cutPoint.y);
        }
        
        //Construct the tree
        _left = new BSPNode(left, !_verticalSliced);
        _right = new BSPNode(right, !_verticalSliced);
        

        
        // Examine the criterias
        // Then process the children
        
        _left.Process(rooms, maxSize);
        _right.Process(rooms, maxSize);
        
        // if (_left.Room.size.x * left.size.y <= maxSize)
        // {
        //     rooms.Add(left);
        // }
        // else
        // {
        //     _left.Process(rooms, maxSize);
        // }
        // if (_right.Room.size.x * right.size.y <= maxSize)
        // {
        //     rooms.Add(right);
        // }
        // else
        // {
        //     _right.Process(rooms, maxSize);
        // }
    }
}