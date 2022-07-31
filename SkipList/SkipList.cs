using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SkipList
{
    class Node<T> where T : IComparable<T>
    {

        public T value;
        public Node<T> next { get; set; }
        public Node<T> prev { get; set; }

        public int Height { get; set; }


        public Node<T> down { get; set; }

        public Node()
        {

        }



        public Node(T value, int Height)
        {
            this.value = value;
            this.Height = Height;
        }
    }

    class SkipList<T> : ICollection<T>
        where T : IComparable<T>
    {
        Random random;
        public int Count { get; private set; }
        public bool IsReadOnly => false;
        public Node<T> head;
        public SkipList(Random random)
        {
            this.random = random;
            head = new Node<T>(default, 1);
        }
        private int getRandomHeight()
        {
            int temp = 1;
            while (temp <= head.Height)
            {
                if (random.Next(1, 3) == 1)
                {
                    temp++;
                }
                else
                {
                    return temp;
                }
            }
            return temp;
        }
        public void ResizeHead(int oldHeight, int NewHeight)
        {
            if (oldHeight >= NewHeight)
            {
                return;
            }
            else
            {
                Node<T> newHead = new Node<T>(head.value, NewHeight);
                newHead.down = head;
                head = newHead;
            }
        }
        public void Add(T value)
        {
            int newHeight = getRandomHeight();
            ResizeHead(head.Height, newHeight);
            Node<T> current = head;
            while (current.Height > newHeight)
            {
                current = current.down;
            }

            Node<T> lastInserted = null;
            while (current != null)
            {

                //another loop for finding where to insert in hte current doubly linked list
                while (current.next != null && value.CompareTo(current.next.value) > 0)
                {
                    current = current.next;
                }
                if (current.next == null)
                {
                    Node<T> Inserted = new Node<T>(value, current.Height);
                    current.next = Inserted;
                    Inserted.prev = current;

                    if (lastInserted == null)
                    {
                        lastInserted = Inserted;
                    }
                    else
                    {
                        lastInserted.down = Inserted;
                        lastInserted = Inserted;
                    }
                }
                else
                {

                    Node<T> Inserted = new Node<T>(value, current.Height);
                    Inserted.prev = current;
                    Inserted.next = current.next;
                    current.next = Inserted;
                    if (current.prev != null)
                    {

                        Inserted.next.prev = Inserted;
                    }
                    if (lastInserted == null)
                    {
                        lastInserted = Inserted;
                    }
                    else
                    {
                        lastInserted.down = Inserted;
                        lastInserted = Inserted;
                    }
                }
                current = current.down;

            }

            Count++;

        }
        public void Clear()
        {
            Count = 0;
            head.next = null;
            head = new Node<T>(default, 1);
        }
        public bool Contains(T item)
        {
            return Search(item) != null;
        }

        public Node<T> Search(T item)
        {
            Node<T> current = head;
            while (current.value.CompareTo(item) != 0)
            {
                while (current.next != null)
                {
                    current = current.next;
                }
                if ((current.next == null && current.down != null) || current.next.value.CompareTo(item) > 0)
                {
                    current = current.down;
                }
            }
            return current; 
            //while (current.next != null && current.down != null || current.value.CompareTo(item) == 0)
            //{
            //    if (current.next.value.CompareTo(item) > 0)
            //    {
            //        current = current.next;
            //    }
            //    else 
            //    {
            //        current = current.down;
            //    }
            //}
            //while (current != null)
            //{
            //    if (current.value.CompareTo(item) == 0)
            //    {
            //        return current;
            //    }
            //    current = current.next;
            //}
            //return null;
        }
        public bool Remove(T item)
        {
            Node<T> deleted = Search(item);
            while (Contains(item) == true)
            {
                if (deleted.next != null)
                {
                    deleted.prev.next = deleted.next;
                    deleted.next.prev = deleted.prev;
                    
                }
                else
                {
                    deleted.prev.next = null; 
                }
                if (deleted.down != null)
                {
                    deleted = deleted.down;
                }
            }
            Count--; 
            return true; 
        }


        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }


        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
