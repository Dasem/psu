class Test
  
  attr_accessor :name

  def initialize(name = "World")
    @name = name
  end

  def kulity
    puts("Hi, #{@name.capitalize}")
  end

  def bb
    puts("bb, #{@name.capitalize}")
  end
end


lol = Test.new("tupa chelik")

lol.name = "kek"

lol.kulity
lol.bb
