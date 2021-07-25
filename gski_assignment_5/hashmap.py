from bucket import Bucket


class HashMap:
    def __init__(self):
        self.map_size = 8
        self.map_arr = [Bucket() for _ in range(self.map_size)]
        self.total_arr_size = 0

    def __setitem__(self, key, data):
        if not self.map_arr[self.__compress(key)].contains(key):
            self.total_arr_size += 1
        self.map_arr[self.__compress(key)][key] = data

    def __getitem__(self, key):
        return self.map_arr[self.__compress(key)][key]

    def __len__(self):
        return self.total_arr_size

    def __check_size(self):
        if (self.map_size * 1.2) <= self.total_arr_size:
            return True
        return False

    def insert(self, key, data):
        if self.__check_size():
            self.rebuild()
        self.map_arr[self.__compress(key)].insert(key, data)
        self.total_arr_size += 1

    def rebuild(self):
        self.map_size = self.map_size * 2
        new_map_arr = [Bucket() for _ in range(self.map_size)]
        for bucket in self.map_arr:
            first_node = bucket.head.next
            while first_node.data is not None:
                new_map_arr[self.__compress(first_node.key)].insert(first_node.key, first_node.data)
                first_node = first_node.next

        self.map_arr = new_map_arr

    def update(self, key, data):
        self.map_arr[self.__compress(key)].update(key, data)

    def find(self, key):
        return self.map_arr[self.__compress(key)].find(key)

    def contains(self, key):
        return self.map_arr[self.__compress(key)].contains(key)

    def remove(self, key):
        self.map_arr[self.__compress(key)].remove(key)
        self.total_arr_size -= 1

    def __compress(self, key):
        return hash(key) % self.map_size
