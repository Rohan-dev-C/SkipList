using System;
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

    class SkipList<T> where T : IComparable<T>
    {
        Random random;

        public int Count { get; private set; }
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

        public void Insert(T value)
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
                while (value.CompareTo(current.value) > 0 && current.next != null)
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
                else //This does not work, inserting at the end works
                {

                    Node<T> Inserted = new Node<T>(value, current.Height);
                    Inserted.prev = current.prev;
                    Inserted.next = current.next;

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


    }
}
