class ItemExistsException(Exception):
    pass


class NotFoundException(Exception):
    pass


class Node:
    def __init__(self):
        self.key = None
        self.data = None
        self.next = None


class Bucket:
    def __init__(self):
        self.head = Node()
        self.tail = Node()

        self.head.next = self.tail

        self.size = 0

    def __setitem__(self, key, data):
        found_node = self.search(key)
        if found_node:
            found_node.data = data
        else:
            self.insert(key, data, True)

    def __getitem__(self, key):
        return self.find(key)

    def __len__(self):
        return self.size

    def insert(self, key, data, node_found=False):  # node_found is used in case __setitem__ is calling insert()

        if node_found is True or not self.search(key):
            self.size += 1
            next_node = self.head.next

            new_node = Node()
            new_node.data = data
            new_node.key = key

            new_node.next = next_node

            self.head.next = new_node
        else:
            raise ItemExistsException()

    def update(self, key, data):
        found_node = self.search(key)
        if found_node:
            found_node.data = data
        else:
            raise NotFoundException()

    def find(self, key):
        found_node = self.search(key)
        if found_node:
            return found_node.data
        else:
            raise NotFoundException()

    def contains(self, key):
        if self.search(key):
            return True
        else:
            return False

    def remove(self, key):
        found_node = self.search(key, True)
        if found_node:
            if found_node.key == key:  # For when there is only 1 node in the list
                self.head.next = None

            else:
                to_remove_node = found_node.next
                found_node.next = to_remove_node.next

            self.size -= 1

        else:
            raise NotFoundException()

    def search(self, key, if_remove=False, selected_node=None):  # if if_remove is True then the function returns the
        # node in front of the requested node
        if self.size > 0:

            if selected_node is None:
                selected_node = self.head.next

            if selected_node.data is not None:
                if if_remove and selected_node.next.key == key:
                    return selected_node

                if selected_node.key == key:
                    return selected_node
                else:
                    return self.search(key, if_remove, selected_node.next)

        return False
