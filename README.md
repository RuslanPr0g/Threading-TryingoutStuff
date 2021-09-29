# Threading-TryingoutStuff
With .NET, you can write applications that perform multiple operations at the same time. Operations with the potential of holding up other operations can execute on separate threads, a process known as multithreading or free threading.

## Lock keyword

In the case of using several threads, you have to coordinate their actions, such a process is called synchronization.
<br /><br />
The main reason for synchronization is the need to share among several or more common resource (shared resource), which can be available for one thread at a time.
<br /><br />
The synchronization contains the concept of blocking, by means of which the access control to the code block (critical section) is organized. When an object (access synchronization object) is locked by one thread, the other threads cannot access to a locked code block (critical section). When the lock is released by one thread, the object (access synchronization object) becomes available for use in another thread.
<br /><br />
The object of synchronization of access (access synchronization object) to a shared resource is an object that represents the resource being synchronized. In some cases they are provided with an instance of the resource itself or an arbitrary instance of the class used for comparison.
<br /><br />
The lock keyword prevents one thread from entering a critical section of code while another thread is in it. When trying to enter another thread in the locked code, you will need to wait until the object is unlocked.
