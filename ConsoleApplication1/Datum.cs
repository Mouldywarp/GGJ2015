using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTN
{
    public abstract class DataNode
    {
        public enum DataType { OBJ, INT, FLOAT, BOOL }
        protected DataType _type;
        protected int _id;
        protected DataNode _nextSibling; // next sibling in tree

        public DataType GetType() { return _type; }
    }

    // A node in the tree with children, e.g. represents an "object" that has multiple attributes
    public class DataObject : DataNode
    {
        public DataObject(int id)
        {
            _id = id;
            _type = DataType.OBJ;
        }

    }

    // A node with no children, e.g. represent a value
    public class DataInt : DataNode
    {
        public DataInt(int id, int value)
        {
            _id = id;
            _type = DataType.INT;
            _value = value;
        }

        int _value;
    }
    public class DataFloat : DataNode
    {
         public DataFloat(int id, float value)
        {
            _id = id;
            _type = DataType.FLOAT;
            _value = value;
        }

        float _value;
    }
    public class DataBool : DataNode
    {
         public DataBool(int id, bool value)
        {
            _id = id;
            _type = DataType.BOOL;
            _value = value;
        }

        bool _value;
    }

    
}
